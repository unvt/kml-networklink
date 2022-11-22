<?php
mb_language('Japanese');
mb_internal_encoding('UTF-8');
mb_regex_encoding('UTF-8');
ini_set('default_charset', 'UTF-8');
ini_set('mbstring.substitute_character', 'none');
date_default_timezone_set("Asia/Tokyo");



/*

＜コマンドパラメータ＞
	-p ..... htmlフォルダのパス（必須）
	-u ..... kmlフォルダのURL（必須）
	-c ..... 経過メッセージ出力文字コード(Shift-JIS|UTF-8 ほか)（省略可：既定：UTF-8）
	-l ..... 更新履歴の保持日数（省略可：既定：削除しない）

*/



$my_file_path = str_replace('\\', '/', __FILE__);



// コマンドパラメータを取得
$opt = getopt("p:u:c:l:");

// 経過メッセージ出力文字コード
$PARAM_OUTPUT_CODE = (isset($opt["c"]) ? $opt["c"] : 'UTF-8');

// 更新履歴の保持日数
$PARAM_INFO_LIMIT = (isset($opt["l"]) && is_numeric($opt["l"]) ? $opt["l"] : -1);

// htmlフォルダの場所
$PARAM_HTML_DIR = trim($opt["p"], '/\\');

// htmlフォルダのURL
$PARAM_KML_BASE_URL = $opt["u"];
$PARAM_KML_BASE_URL = ( preg_match('/\/$/', $PARAM_KML_BASE_URL) ? $PARAM_KML_BASE_URL : $PARAM_KML_BASE_URL . '/' );

// 必須パラメータチェック
if ( !$PARAM_HTML_DIR || !$PARAM_HTML_DIR )
{
	outputlog("not found parameters -p[path] -u[url]\r\n");
	exit;
}



// dataフォルダへ保存するデータのフォルダを指定
$b_data_dir = dirname($my_file_path)."";
$r_tmpl_dir = $b_data_dir."/tmpl";

// htmlフォルダへ保存するデータのフォルダを指定
$w_kml_dir  = $PARAM_HTML_DIR."/kml";
$w_json_dir = $PARAM_HTML_DIR."/js";

// 各種ファイルパスを指定
$r_tmpl_kml_filepath  = $r_tmpl_dir."/layer.kml";
$r_tmpl_html_filepath = $r_tmpl_dir."/index.html";
$w_html_filepath = $PARAM_HTML_DIR."/index.html";
$w_json_filepath = $w_json_dir."/gsi_kmllist.js";
$b_ids_filepath  = $b_data_dir."/layers_ids.txt";
$w_info_filepath = $b_data_dir."/layers_info.txt";
$w_white_filepath = $b_data_dir."/layers_whites.txt";

// layers.txtのURLが格納されているファイルのパスを指定
$r_layers_url_filepath = $b_data_dir."/layers_url.txt";

// layers_desc.txt（レイヤーの説明ファイル）が格納されているファイルのパスを指定
$r_layers_desc_filepath = $b_data_dir."/layers_desc.txt";





// テンプレートHTML中のデリミタ
$TMPL_UPDATE_DELIMITER = '<!--___TMPL_UPDATE_DELIMITER___-->';
$TMPL_TAB_DELIMITER    = '<!--___TMPL_TAB_DELIMITER___-->';
$TMPL_GROUP_DELIMITER  = '<!--___TMPL_GROUP_DELIMITER___-->';
$TMPL_TAG_DELIMITER    = '<!--___TMPL_TAG_DELIMITER___-->';
$TMPL_LAYER_DELIMITER  = '<!--___TMPL_LAYER_DELIMITER___-->';





outputlog("gsikmlupdate\r\n");





// layers_ids.txtの読み込み
$layers_ids = array();
if ( file_exists($b_ids_filepath) )
{
	$layers_ids_temp = file_get_contents($b_ids_filepath);
	$layers_ids_list = explode("\r\n", $layers_ids_temp);
	
	for ( $i=0; $i<count($layers_ids_list); $i++ )
	{
		if ( trim($layers_ids_list[$i]) )
		{
			$items = explode(",", $layers_ids_list[$i]);
			if ( count($items) > 0 && trim($items[0]) )
			{
				$layers_ids[] = trim($items[0]);
			}
		}
	}
}


// layers_url.txtの読み込み
$layers_txt_temp = file_get_contents($r_layers_url_filepath);
$layers_txt = explode("\r\n", $layers_txt_temp);

for ( $i=0; $i<count($layers_txt); $i++ )
{
	if ( !$layers_txt[$i] )
	{
		array_splice($layers_txt, $i, 1);
	}
}





$error = "";
$layers_all_count = 0;
$layers = [];



// kmlテンプレートの読み込み
if ( !$error )
{
	$tmpl_kml = file_get_contents($r_tmpl_kml_filepath);
	if ( !$tmpl_kml )
	{
		$error = "Failed to read KML template.";
	}
}


// htmlテンプレートの読み込み
if ( !$error )
{
	$tmpl_html = file_get_contents($r_tmpl_html_filepath);
	
	if ( !$tmpl_html )
	{
		$error = "Failed to read HTML template.";
	}
}


// layers_desc.txtの読み込み
if ( !$error )
{
	$layers_desc = array();
	if ( file_exists($r_layers_desc_filepath) )
	{
		$desc_txt = file_get_contents($r_layers_desc_filepath);
		$desc_arr = explode("<___ID_SEPARATOR___>", $desc_txt);
		for ( $i=0; $i<count($desc_arr); $i++ )
		{
			$temp = trim($desc_arr[$i]);
			if ( $temp )
			{
				$items = explode("\r\n", $temp);
				$key = trim($items[0]);
				$val = "";
				for ( $j=1; $j<count($items); $j++ )
				{
					$val .= $items[$j] . "\r\n";
				}
				if ( $key )
				{
					$layers_desc[$key] = trim($val);
				}
			}
		}
	}
}


// layers_txtの読み込み
$result_whites = [];

if ( !$error )
{
	//for ( $i=0; $i<1; $i++ )
	for ( $i=0; $i<count($layers_txt); $i++ )
	{
		outputlog("Begin layers_txt => " . basename($layers_txt[$i]) . "\r\n");
		
		$obj = get_layerstxt($layers_txt[$i]);
		if ( preg_match('/\/layers0\.txt$/', $layers_txt[$i]) )
		{
			$obj = array(array("type"=>"LayerGroup", "title"=>"地図", "entries"=>$obj));
		}
		$layers = array_merge_recursive($layers, $obj);
	}
}


// html, kmlファイル作成
if ( !$error )
{
	outputlog("Begin create data files.\r\n");
	
	
	
	// html（タブ）
	$tmplArr = explode($TMPL_TAB_DELIMITER, $tmpl_html);
	$tabs_html = "";
	for ( $i=0; $i<count($layers); $i++ )
	{
		$tab_temp = $tmplArr[1];
		$tab_temp = str_replace("___DATA_TAB___", htmlencode($layers[$i]["title"]), $tab_temp);
		$tab_temp = str_replace("___DATA_TAB_CLASS___", "tab", $tab_temp);
		$tabs_html .= $tab_temp;
	}
	$tab_temp = $tmplArr[1];
	$tab_temp = str_replace("___DATA_TAB___", "　全て　", $tab_temp);
	$tab_temp = str_replace("___DATA_TAB_CLASS___", "tab active all", $tab_temp);
	$tabs_html .= $tab_temp;
	
	$tmpl_html = $tmplArr[0] . $tabs_html . $tmplArr[2];
	
	
	
	// html（グループ）
	$tmpl_html_parts = explode($TMPL_GROUP_DELIMITER, $tmpl_html);
	$tmpl_html_data = $tmpl_html_parts[1];
	
	$result_ids  = [];
	$result_add  = [];
	$result_json = [];
	$result_html = "";
	
	put_layerstxt_entries($layers, "");
	
	// html（件数）
	$tmpl_html_parts[0] = str_replace("___DATA_ALLCOUNT___", $layers_all_count, $tmpl_html_parts[0]);
	
	// html（グループ　ここまで）
	$result_html = $tmpl_html_parts[0] . $result_html . $tmpl_html_parts[2];
	
	
	
	// 更新情報ファイルへ追加
	if ( !$error )
	{
		if ( file_put_contents( $w_info_filepath, implode("", $result_add), FILE_APPEND ) === false )
		{
			$error = "Failed to create of UpdateInfo.\r\n";
		}
	}
	
	// 更新情報取得　古いものは削除
	if ( !$error )
	{
		if ( file_exists($w_info_filepath) )
		{
			$info_temp = file_get_contents($w_info_filepath);
			$info_list = explode("\r\n", $info_temp);
			$new_info_list = array();
			for ( $i=0; $i<count($info_list); $i++ )
			{
				if ( trim($info_list[$i]) )
				{
					$items = explode(",", $info_list[$i]);
					if ( count($items) >= 4 && is_date($items[0]) )
					{
						if ($PARAM_INFO_LIMIT == -1 || strtotime($items[0]) >= strtotime(-1 * $PARAM_INFO_LIMIT - 1 . " day") )
						{
							$new_info_list[] = $info_list[$i] . "\r\n";
						}
					}
				}
			}
			if ( file_put_contents( $w_info_filepath, implode("", $new_info_list) ) === false )
			{
				$error = "Failed to rebuild of UpdateInfo.\r\n";
			}
			else
			{
				$info_list = $new_info_list;
			}
		}
	}
	
	// 更新情報を置換
	if ( !$error )
	{
		$info_html_parts = explode($TMPL_UPDATE_DELIMITER, $result_html);
		$result_info = "";
		for ( $i=count($info_list)-1; $i>=0; $i-- )
		{
			if ( trim($info_list[$i]) )
			{
				$items = explode(",", $info_list[$i]);
				
				$comm = "「" . csvdecode($items[2]) . "」が追加されました。";
				$pan  = str_replace("#dlmt#", "&nbsp;&gt;&nbsp;", $items[3]);
				
				$result_temp = $info_html_parts[1];
				$result_temp = str_replace("___UPDATE_DATE___", outputdate($items[0], '.'), $result_temp);
				$result_temp = str_replace("___UPDATE_COMM___", csvdecode($comm), $result_temp);
				$result_temp = str_replace("___UPDATE_PANKUZU___", csvdecode($pan), $result_temp);
				$result_info .= $result_temp;
			}
		}
		$result_html = $info_html_parts[0] . $result_info . $info_html_parts[2];
	}
	
	// htmlファイル作成
	if ( !$error )
	{
		if ( file_put_contents_with_backup( $w_html_filepath, $result_html ) === false )
		{
			$error = "Failed to create of html file.\r\n";
		}
	}
	
	// idsファイル作成
	if ( !$error )
	{
		if ( file_put_contents( $b_ids_filepath, implode("\r\n", $result_ids) ) === false )
		{
			$error = "Failed to create of ids file.\r\n";
		}
	}
	
	// baseurlsファイル作成（ホワイトリスト）
	if ( !$error )
	{
		if ( file_put_contents( $w_white_filepath, implode("\r\n", $result_whites) ) === false )
		{
			$error = "Failed to create of WhiteList file.\r\n";
		}
	}
	
	// jsonファイル作成
	/*
	if ( !$error )
	{
		if ( file_put_contents( $w_json_filepath, json_encode($result_json, JSON_UNESCAPED_UNICODE) ) === false )
		{
			$error = "jsonファイルの作成に失敗しました。\r\n";
		}
	}
	*/
	
	// kmlファイル整理
	if ( !$error )
	{
		$temp_ids = $result_ids;
		foreach ( glob($w_kml_dir . "/gsi_*.kml") as $kml_filepath )
		{
			$exist = false;
			$kml_filename = basename($kml_filepath);
			
			for ( $i=0; $i<count($temp_ids); $i++ )
			{
				$temp_parts = explode(",", $temp_ids[$i]);
				if ( count($temp_parts) > 0 && trim($temp_parts[0]) )
				{
					$temp_id = trim($temp_parts[0]);
					$temp_filename = get_kmlfilename_by_id($temp_id);
					
					if ( $kml_filename == $temp_filename )
					{
						$exist = true;
						$ex = array_splice($temp_ids, $i, 1);
						break;
					}
				}
			}
			
			// idsファイルに存在しないkmlは削除する
			if ( !$exist )
			{
				//echo "DEL: " . $kml_filepath . "\r\n";
				unlink($kml_filepath);
			}
		}
	}
}


if ( $error )
{
	outputlog("エラー：" . $error);
}
else
{
	outputlog("End\r\n");
}
exit;




/**********************************************

layers_txt の取得

**********************************************/
function get_layerstxt($url, $error="")
{
	echo "SRC Load => " . $url . " ... ";
	$content = file_get_contents($url);
	
	if ( $content === FALSE )
	{
		echo "FAILED.\r\n";
		$error = "failed at Load '" . $url . "'";
		return [];
	}
	
	
	$obj = json_decode(trim($content), true);
	
	if ( !isset($obj["layers"]) )
	{
		echo "NO DATA\r\n";
		return [];
	}
	echo "OK\r\n";
	
	
	$obj["layers"] = get_layerstxt_entries($obj["layers"], dirname($url));
	
	
	return $obj["layers"];
}

function get_layerstxt_entries($entries, $baseurl, $error="")
{
	for ( $i=0; $i<count($entries); $i++ )
	{
		$layer = $entries[$i];
		if ( $layer["type"] == "LayerGroup" )
		{
			if ( isset($layer["src"]) )
			{
				$url = $baseurl;
				$src = $layer["src"];
				
				if ( !preg_match("/^http/", $src) )
				{
					while( preg_match("/^\./", $src ) )
					{
						if ( preg_match("/^\.\.\//", $src) )
						{
							$src = preg_replace("/^\.\.\//", "", $src);
							$url = dirname($url);
						}
						elseif ( preg_match("/^\.\//", $src) )
						{
							$src = preg_replace("/^\.\//", "", $src);
						}
					}
					$src = $url."/".$src;
				}
				$layer["entries"] = get_layerstxt( $src );
			}
			
			if ( isset($layer["entries"]) )
			{
				$layer["entries"] = get_layerstxt_entries($layer["entries"], $baseurl);
			}
		}
		$entries[$i] = $layer;
	}
	
	return $entries;
}



/**********************************************

ファイル作成

**********************************************/
function put_layerstxt_entries($entries, $pankuzu)
{
	global $w_kml_dir;
	global $tmpl_kml;
	global $tmpl_html_data;
	global $layers_desc;
	global $layers_ids;
	global $result_whites;
	global $result_html;
	global $result_json;
	global $result_ids;
	global $result_add;
	global $layers_all_count;
	global $PARAM_KML_BASE_URL;
	global $TMPL_TAG_DELIMITER;
	global $TMPL_LAYER_DELIMITER;
	
	if ( count($entries) == 0 ) return;
	
	$pankuzu_delimiter = "#dlmt#";
	$pankuzu = preg_replace('/^' . $pankuzu_delimiter . '/', '', $pankuzu);
	
	
	
	// レイヤー
	$tmpl_layer_parts = explode($TMPL_LAYER_DELIMITER, $tmpl_html_data);
	$tmpl_layer_head = $tmpl_layer_parts[0];
	$tmpl_layer_data = $tmpl_layer_parts[1];
	$tmpl_layer_foot = $tmpl_layer_parts[2];
	
	
	
	// タグ
	$tmpl_tag_parts = explode($TMPL_TAG_DELIMITER, $tmpl_layer_head);
	$pankuzu_html = "";
	if ( $pankuzu )
	{
		$pankuzu_arr = explode($pankuzu_delimiter, $pankuzu);
		
		for ( $i=0; $i<count($pankuzu_arr); $i++ )
		{
			$pankuzu_temp = $tmpl_tag_parts[1];
			$pankuzu_temp = str_replace("___DATA_TAG___", htmlencode($pankuzu_arr[$i]), $pankuzu_temp);
			$pankuzu_html .= $pankuzu_temp;
		}
	}
	$tmpl_layer_head = $tmpl_tag_parts[0] . $pankuzu_html . $tmpl_tag_parts[2];
	
	
	
	$layers_result_html = "";
	
	for ( $i=0; $i<count($entries); $i++ )
	{
		$layer = $entries[$i];
		
		if ( $layer["type"] == "Layer" )
		{
			$layer["id"] = trim($layer["id"]);
			
			$data_xytype = 0;
			$matches = array("", "", "");
			if ( preg_match('<^(.*/)\{z\}/\{x\}/\{x\}\-\{y\}\-\{z\}\.([0-9a-zA-Z]+)$>', $layer["url"], $matches) )
			{
				$data_xytype = 1;
			}
			elseif ( preg_match('<^(.*/)\{z\}/\{x\}/\{y\}\.([0-9a-zA-Z_]+)$>', $layer["url"], $matches) )
			{
				$data_xytype = 2;
			}
			else
			{
				continue;
			}
			$data_baseurl   = $matches[1];
			$data_imagetype = $matches[2];
			
			if ( !$data_baseurl || !$data_imagetype || !preg_match('/^(jpg|jpeg|png|gif|bmp)$/i', $data_imagetype) )
			{
				continue;
			}
			
			if ( !preg_match('/^[a-zA-Z0-9\_\-]+$/', $layer["id"]) )
			{
				continue;
			}
			
			
			$data_zmin = 0;
			if ( isset($layer["minZoom"]) && is_numeric($layer["minZoom"]) )
			{
				$data_zmin = $layer["minZoom"];
			}
			
			
			$data_zmax = 18;
			if ( isset($layer["maxZoom"]) && is_numeric($layer["maxZoom"]) )
			{
				$data_zmax = $layer["maxZoom"];
			}
			
			
			$result_whites[] = $data_baseurl;
			
			
			$kml = $tmpl_kml;
			$kml = str_replace("___DATA_NAME___", htmlencode(br2crlf($layer["title"])), $kml);
			$kml = str_replace("___DATA_XYTYPE___", htmlencode($data_xytype), $kml);
			$kml = str_replace("___DATA_ZMIN___", $data_zmin, $kml);
			$kml = str_replace("___DATA_ZMAX___", $data_zmax, $kml);
			$kml = str_replace("___DATA_IMAGETYPE___", htmlencode($data_imagetype), $kml);
			$kml = str_replace("___DATA_BASEURL___", urlencode($data_baseurl), $kml);
			
			$filename = get_kmlfilename_by_id($layer["id"]);
			file_put_contents($w_kml_dir . '/' . $filename, $kml);
			
			
			$href = $PARAM_KML_BASE_URL . htmlencode($filename);
			
			$html = $tmpl_layer_data;
			$html = str_replace("___DATA_LAYERID___", htmlencode($layer["id"]), $html);
			$html = str_replace("___DATA_LAYERNAME___", htmlencode(br2crlf($layer["title"])), $html);
			$html = str_replace("___DATA_LAYERDESC___", ( isset($layers_desc[ $layer["id"] ]) ? $layers_desc[ $layer["id"] ] : "" ), $html);
			$html = str_replace("___DATA_LAYERHREF___", $href, $html);
			
			$layers_result_html .= $html;
			
			
			foreach ($layer as $key => $val)
			{
				if ( !preg_match('/^(id|title)$/i', $key) )
				{
					unset($layer[$key]);
				}
			}
			
			$layer["pankuzu"] = $pankuzu;
			$layer["desc"] = ( isset($layers_desc[ $layer["id"] ]) ? $layers_desc[ $layer["id"] ] : "" );
			$result_json[] = $layer;
			$result_ids[] = $layer["id"];
			
			if ( !in_array($layer["id"], $layers_ids) )
			{
				$result_add[] = date("Y/m/d") . "," . csvencode($layer["id"]) . "," . csvencode($layer["title"]) . "," . csvencode($pankuzu) . "\r\n";
			}
			
			
			$layers_all_count++;
		}
	}
	
	if ( $layers_result_html )
	{
		$result_html .= $tmpl_layer_head . $layers_result_html . $tmpl_layer_foot;
	}
	
	for ( $i=0; $i<count($entries); $i++ )
	{
		$layer = $entries[$i];
		
		if ( $layer["type"] == "LayerGroup" )
		{
			put_layerstxt_entries($layer["entries"], $pankuzu . $pankuzu_delimiter . $layer["title"]);
		}
	}
}



function get_kmlfilename_by_id( $id )
{
	return 'gsi_' . $id . '.kml';
}



/**********************************************

バックアップ

**********************************************/
function file_put_contents_with_backup( $filepath, $result_html, $limit=3 ) {
	$dir = dirname($filepath);
	$name = basename($filepath);
	
	$temp_filepath = get_backup_filepath($filepath, 0);
	
	// 一時保存
	if ( file_put_contents($temp_filepath, $result_html) )
	{
		// 不要なbackupを削除
		foreach ( glob( get_backup_filepath($filepath, "*") ) as $del_filepath )
		{
			$del_filename = basename($del_filepath);
			if ( preg_match("/^" . $name . "\.([0-9]+?)\.gsibackup$/", $del_filename, $matches) )
			{
				if ( $matches[1] > $limit )
				{
					//echo $name . " =====> " . $matches[1] . "\r\n";
					unlink( $del_filepath );
				}
			}
		}
		
		// backupをリネーム
		for ( $i=$limit-1; $i>=1; $i-- )
		{
			$before = get_backup_filepath($filepath, $i);
			$after  = get_backup_filepath($filepath, $i+1);
			if ( file_exists($before) )
			{
				rename( get_backup_filepath($filepath, $i), get_backup_filepath($filepath, $i+1) );
			}
		}
		
		// 現行ファイルをリネーム
		if ( !file_exists($filepath) || rename( $filepath, get_backup_filepath($filepath, 1) ) )
		{
			// 一時ファイルをリネーム
			if ( rename($temp_filepath, $filepath) )
			{
				return true;
			}
		}
	}
	return false;
}



function get_backup_filepath( $filepath, $number )
{
	$dir = dirname($filepath);
	$name = basename($filepath);
	
	return $dir . "/" . $name . "." . $number . ".gsibackup";
}



/**********************************************

汎用

**********************************************/
function br2crlf( $str, $crlf = "\n" )
{
	return str_replace( "<br>", $crlf, $str );
}



function htmlencode( $str, $br_flg = false )
{
	$result = htmlentities( $str, ENT_QUOTES, "UTF-8" );
	
	if ( $br_flg )
	{
		$result = str_replace( "\r\n", "\n", $result );
		$result = str_replace( "\r", "\n", $result );
		$result = str_replace( "\n", "<br>", $result );
	}
	
	return $result;
}



function csvdecode( $str )
{
	$result = $str;
	$result = str_replace("<<quot>>", "\"", $result);
	$result = str_replace("<<comma>>", ",", $result);
	$result = str_replace("<<crlf>>", "\r\n", $result);
	return $result;
}



function csvencode( $str )
{
	$result = $str;
	$result = str_replace("\"", "<<quot>>", $result);
	$result = str_replace(",", "<<comma>>", $result);
	$result = str_replace("\r\n", "\n", $result);
	$result = str_replace("\r", "\n", $result);
	$result = str_replace("\n", "<<crlf>>", $result);
	return $result;
}



function is_date( $date )
{
	$date  = str_replace(array('-', '.'), '/', $date);
	$dates = explode('/', $date);
	if ( count($dates) != 3 ) return false;
	
	$y = $dates[0];
	$m = $dates[1];
	$d = $dates[2];
	
	if ( !is_numeric($y) || !is_numeric($m) || !is_numeric($d) )
	{
		return false;
	}
	elseif ( !checkdate($m, $d, $y) )
	{
		return false;
	}
	else
	{
		return true;
	}
}



function outputdate( $date, $separator )
{
	if ( is_date($date) )
	{
		return str_replace(array('-', '.', '/'), $separator, $date);
	}
	else
	{
		return $date;
	}
}



function outputlog( $str )
{
	global $PARAM_OUTPUT_CODE;
	echo mb_convert_encoding($str, $PARAM_OUTPUT_CODE, 'UTF-8');
}



