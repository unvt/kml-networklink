<?php


class AccessLog
{
	
	
	
	/************************************************
	 
	 layers_txt中に存在するidであれば真を返す
	 
	************************************************/
	public function IsGsiId( $id, $idsFilePath )
	{
		$result = false;
		$dataAll = "";
		$dataLines = array();
		
		if ( file_exists($idsFilePath) )
		{
			$dataAll = file_get_contents($idsFilePath);
			$dataAll = str_replace("\r\n", "\n", $dataAll);
			$dataAll = str_replace("\r", "\n", $dataAll);
			$dataLines = explode("\n", $dataAll);
		}
		
		for ( $i=0; $i<count($dataLines); $i++ )
		{
			if ( $dataLines[$i] != "" )
			{
				$line = trim($dataLines[$i]);
				$items = explode(",", $line);
				if ( count($items) > 0 )
				{
					if ( trim($items[0]) == trim($id) )
					{
						$result = true;
					}
				}
			}
		}
		
		return $result;
	}
	
	
	
	/************************************************
	 
	 ログファイルを保存する
	 
	************************************************/
	public function SaveLog( $logFolderPath, $logFilePrefix, $reqId )
	{
		// 今日の日付
		$d = new DateTime();
		$yearString = $d->format('Y');
		$dateString = $d->format('Y/m/d');
		$timeString = $d->format('H:i:s');
		
		// 年フォルダが存在しない場合は作成
		$yearFolderPath = $logFolderPath . "\\" . $yearString;
		if ( !file_exists($yearFolderPath) )
		{
			mkdir($yearFolderPath);
		}
		
		$logFilePath = $yearFolderPath . "\\" . $logFilePrefix . $d->format("Y-m-d") . ".txt";
		
		// CSVヘッダ
		$headLine = "";
		$headLine .= "DATE,";
		$headLine .= "TIME,";
		$headLine .= "ID,";
		$headLine .= "REMOTE_ADDR,";
		$headLine .= "HTTP_USER_AGENT";
		
		// 書き込むデータ
		$dataLine = "";
		$dataLine .= $this->CsvEncode($dateString) . ",";
		$dataLine .= $this->CsvEncode($timeString) . ",";
		$dataLine .= $this->CsvEncode($reqId) . ",";
		$dataLine .= $this->CsvEncode($_SERVER["REMOTE_ADDR"]) . ",";
		$dataLine .= $this->CsvEncode($_SERVER["HTTP_USER_AGENT"]) . "\r\n";
		
		// ファイルが存在しない場合は作成してヘッダ行を書き込む
		$isWriteHead = false;
		if ( !file_exists($logFilePath) )
		{
			$isWriteHead = true;
		}
		
		// ファイルへ書き込む
		if ( $isWriteHead )
		{
			$dataLine = $headLine . "\r\n" . $dataLine;
		}
		file_put_contents($logFilePath, $dataLine, FILE_APPEND | LOCK_EX);
		
	}
	
	
	
	/*****************************************************************
	 
	 csv用にエンコードした文字列を返す（ダブルクォートで囲まないcsv）
	 
	*****************************************************************/
	public function CsvEncode( $val )
	{
		$result = $val;
		
		$result = str_replace('"', '<<quot>>', $result);
		$result = str_replace(',', '<<comma>>', $result);
		$result = str_replace("\r\n", "\n", $result);
		$result = str_replace("\r", "\n", $result);
		$result = str_replace("\n", "<<crlf>>", $result);
		
		return $result;
	}
	
	
	
}




?>