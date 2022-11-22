<?php

if(empty($_SERVER["QUERY_STRING"])) exit( "パラメータが送られていません" );

$request=[];
$request['XYTYPE']="";
$request['BASEURL']="";
$request['ZMIN']="";
$request['ZMAX']="";
$request['IMAGETYPE']="";
$request['BBOX']="";

$whitelist="../../manager/data/layers_whites.txt";

foreach (array_keys($_GET) as $key) {
    $request[$key] = $_GET[$key];
}

    $PointXY =[];
    function PointXY(int $_x, int $_y) {
    	global $PointXY;
    	$PointXY['x'] = (int)$_x;
    	$PointXY['y'] = (int)$_y;
    }

    $LatLng =[];
    function LatLng(float $_lat, float $_lng) {
     	global $LatLng;
     	$LatLng['lat'] = (float)$_lat;
    	$LatLng['lng'] = (float)$_lng;
    }
	function ConvLatLng2XY($LatLng, int $z) { // 緯度経度に該当するタイル番号を返します。
   	global $PointXY;
			$ltLatRad = (double)$LatLng['lat'] * M_PI / 180.0;
			$ltLngRad = (double)$LatLng['lng'] * M_PI / 180.0;
			$R = (double)128.0 / M_PI;
			$ltWx = (double)$R * ($ltLngRad + M_PI);
			$ltWy = (double)(-1 * $R) / 2 * Log((1 + Sin($ltLatRad))/ (1 - Sin($ltLatRad))) + 128.0;
			$pcX = (double)$ltWx * Pow(2, $z);
			$pcY = (double)$ltWy * Pow(2, $z);
			$tileXlt = (int)(Floor($pcX / 256.0));
			$tileYlt = (int)(Floor($pcY / 256.0));
			PointXY($tileXlt, $tileYlt);
		}
	function ConvXY2LatLng($PointXY , int $z) { // タイルの左上の座標を返します。
  	global $LatLng;
			$pcX = (int)$PointXY['x'] * 256;
			$pcY = (int)$PointXY['y'] * 256;
			$wcX = (double)$pcX / Pow(2, $z);
			$wcY = (double)$pcY / Pow(2, $z);
			$R = (double)128.0 / M_PI;
			$ltLatRad = (double)Atan(Sinh((128.0 - $wcY) / $R));
			$ltLngRad = (double)$wcX / $R - M_PI;
			$lat = (double)$ltLatRad * 180.0 / M_PI;
			$lng = (double)$ltLngRad * 180.0 / M_PI;
			LatLng($lat, $lng);
	}
	
	function htmlencode( $str, $br_flg = false ) {
		$result = htmlentities( $str, ENT_QUOTES, "UTF-8" );
		if ( $br_flg )
		{
				$result = str_replace( "\r\n", "\n", $result );
				$result = str_replace( "\r", "\n", $result );
				$result = str_replace( "\n", "<br>", $result );
		}
	
	return $result;
	}

ProcessRequest($request);

    function ProcessRequest($request) {
    	global $PointXY;
     	global $LatLng;
     	global $whitelist;

        $xytype=-1;
		if($request["XYTYPE"] != null ) {
			$xytype = (int)$request["XYTYPE"];
		}
		
		$strBaseURL = "";
		$exist=false;
		if($request["BASEURL"] !="") {
			$strBaseURL = UrlDecode($request["BASEURL"]);
			if(file_exists($whitelist)){
				$f=file_get_contents($whitelist);
				$f=explode("\r\n",$f);
				for($i=0;$i<count($f);$i++){
					if(trim($f[$i]))
					{
						if($strBaseURL==$f[$i]){
						//if( preg_match("/^".$f[$i]."/",$strBaseURL)){
							$exist=true;
						}
					
					}
				}
			
			}
		
		}
		if(!$exist){
			echo "取得した値が不正です。\n wrong request";
			return;
		}

		$zmin = -1;
		if($request["ZMIN"] != null) {
			$zmin = (int)$request["ZMIN"];
		}
		$zmax = -1;
		if($request["ZMAX"] != null) {
			$zmax = (int)$request["ZMAX"];
		}
		$strImgExt = "";
		
		if($request["IMAGETYPE"] != null) {
			if (preg_match( "/^(jpg|png|gif|jpeg)$/i",$request["IMAGETYPE"] ) ){
				$strImgExt = $request["IMAGETYPE"];
			}else{
				echo "取得した値が不正です。\n wrong request";
				return;
			}
		}
		$strKMLData = "";
		header("Content-type: text/plain; charset=UTF-8");

		$strKMLData = (string)"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
		$strKMLData = (string)$strKMLData."<kml xmlns=\"http://www.opengis.net/kml/2.2\">\n";
		$strKMLData = (string)$strKMLData."<Document>\n";
		$strKMLData = (string)$strKMLData."	 <Folder>\n";
		
		if($request["BBOX"] != null) {

				$strReqBBOX = $request["BBOX"];
				if(1024 <strlen($strReqBBOX)) {
					echo "リクエストが多すぎます。\n too much request";
					return;
				}
				 $arrayOfstrBBOX =[];
				 $arrayOfstrBBOX = explode(',',$strReqBBOX);
				if(count($arrayOfstrBBOX) == 4) {
					$westLng;
					$southLat;
					$eastLng;
					$northLat;
					try {
						$westLng = (double)$arrayOfstrBBOX[0];
						$southLat = (double)$arrayOfstrBBOX[1];
						$eastLng = (double)$arrayOfstrBBOX[2];
						$northLat = (double)$arrayOfstrBBOX[3];
					}
					catch (Exception $e){
						echo "取得した値が不正です。\n wrong request";
						return;
					}
					// zoomレベルの切り替え
					$dLngBBOX = $eastLng - $westLng;
					$zoom = 0;
					if((879.609376 <= $dLngBBOX) && ($dLngBBOX < 1759.218752)) {
						$zoom = (int)1;
					}
					else if((439.804688 <= $dLngBBOX) && ($dLngBBOX < 879.609376)) {
						$zoom = (int) 2;
					}
					else if((219.902344 <= $dLngBBOX) && ($dLngBBOX < 439.804688)) {
						$zoom = (int) 3;
					}
					else if((109.951172 <= $dLngBBOX) && ($dLngBBOX < 219.902344)) {
						$zoom = (int) 4;
					}
					else if((54.975586 <= $dLngBBOX) && ($dLngBBOX < 109.951172)) {
						$zoom = (int) 5;
					}
					else if((27.487793 <= $dLngBBOX) && ($dLngBBOX < 54.975586)) {
						$zoom = (int) 6;
					}
					else if((13.699951 <= $dLngBBOX) && ($dLngBBOX < 27.487793)) {
						$zoom = (int) 7;
					}
					else if((6.904907 <= $dLngBBOX) && ($dLngBBOX < 13.699951)) {
						$zoom = (int) 8;
					}
					else if((3.4524535 <= $dLngBBOX) && ($dLngBBOX < 6.904907)) {
						$zoom = (int) 9;
					}
					else if((1.72622675 <= $dLngBBOX) && ($dLngBBOX < 3.4524535)) {
						$zoom = (int) 10;
					}
					else if((0.863113375 <= $dLngBBOX) && ($dLngBBOX < 1.72622675)) {
						$zoom = (int) 11;
					}
					else if((0.4315566875 <= $dLngBBOX) && ($dLngBBOX < 0.863113375)) {
						$zoom = (int) 12;
					}
					else if((0.21577834375 <= $dLngBBOX) && ($dLngBBOX < 0.4315566875)) {
						$zoom = (int) 13;
					}
					else if((0.107889171875 <= $dLngBBOX) && ($dLngBBOX < 0.21577834375)) {
						$zoom = (int) 14;
					}
					else if((0.0539445859375 <= $dLngBBOX) && ($dLngBBOX < 0.107889171875)) {
						$zoom = (int) 15;
					}
					else if((0.02697229296875 <= $dLngBBOX) && ($dLngBBOX < 0.0539445859375)) {
						$zoom = (int) 16;
					}
					else if((0.013486146484375 <= $dLngBBOX) && ($dLngBBOX < 0.02697229296875)) {
						$zoom = (int) 17;
					}
					else if(($dLngBBOX < 0.013486146484375)) {
						$zoom = (int) 18;
					}
					else {
						$zoom = (int) 0;
					}

					if(($zmin <= $zoom) && ($zoom <= $zmax)) {
						//左上のタイル番号の取得
						LatLng($northLat, $westLng);
						ConvLatLng2XY($LatLng, $zoom);
						$ltTile = $PointXY;
						//右上のタイル番号の取得
						LatLng($northLat, $eastLng);
						ConvLatLng2XY($LatLng, $zoom);
						$rtTile = $PointXY;
						//右下のタイル番号の取得
						LatLng($southLat, $eastLng);
						ConvLatLng2XY($LatLng, $zoom);
						$rbTile = $PointXY;
						//左上のタイル番号の取得
						LatLng($southLat, $westLng);
						ConvLatLng2XY($LatLng, $zoom);
						$lbTile = $PointXY;
						
						for($i = $ltTile['x']; $i <= $rtTile['x']; $i++) {
							for($j = $ltTile['y']; $j <= $lbTile['y']; $j++) {
								$strKMLData =(string)$strKMLData. "    <GroundOverlay>\n";
								$strKMLData =(string)$strKMLData. "      <name/>\n";
								$strKMLData =(string)$strKMLData. "      <Icon>\n";
								$strTileX = (string)sprintf('%07d', $i);
								$strTileY = (string)sprintf('%07d', $j);
								
								if($xytype == 1) { // {z}/{x}/{x}-{y}-{z}.ext型
									$strKMLData =(string)$strKMLData. "        <href>" . htmlencode($strBaseURL) . (string)$zoom . "/" . (string)$i . "/" . (string)$i . "-" . (string)$j . "-" . (string)$zoom . "." . (string)$strImgExt . "</href>\n";
								}
								else if($xytype == 2) { // {z}/{x}/{y}.ext型
									$strKMLData =(string)$strKMLData. "        <href>" . htmlencode($strBaseURL) . (string)$zoom . "/" . (string)$i . "/" . (string)$j . "." . (string)$strImgExt . "</href>\n";
								}
								$strKMLData =(string)$strKMLData. "      </Icon>\n";
								$strKMLData =(string)$strKMLData. "      <LatLonBox>\n";
								PointXY($i, $j);
								ConvXY2LatLng($PointXY, $zoom);
								$ltLatLng = $LatLng;
								PointXY($i+1, $j+1);
								ConvXY2LatLng($PointXY, $zoom);
								$rbLatLng = $LatLng;
								$strKMLData =(string)$strKMLData. "        <north>" . (string)$ltLatLng['lat'] . "</north>\n";
								$strKMLData =(string)$strKMLData. "        <south>" . (string)$rbLatLng['lat'] . "</south>\n";
								$strKMLData =(string)$strKMLData. "        <east>" . (string)$rbLatLng['lng'] . "</east>\n";
								$strKMLData =(string)$strKMLData. "        <west>" . (string)$ltLatLng['lng'] . "</west>\n";
								$strKMLData =(string)$strKMLData. "      </LatLonBox>\n";
								$strKMLData =(string)$strKMLData. "    </GroundOverlay>\n";
							}
						}
					}
					else {
						$strKMLData =(string)$strKMLData. "    <name>database inaccessible</name>\n";
					}
				}
				else {
					echo "BBOXの値に適正な値が入ってません\n wrong request";
					return;
				}
			}
			//else {
			//	echo "BBOXの値が空です\n wrong request";
			//	return;
			//}		
		
		
		$strKMLData =(string)$strKMLData. "  </Folder>\n";
		$strKMLData =(string)$strKMLData. "</Document>\n";
		$strKMLData =(string)$strKMLData. "</kml>\n";
		
		echo $strKMLData;
			
	}
?>