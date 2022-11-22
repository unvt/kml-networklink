<?php
require_once('cls_config.php');
require_once('cls_accesslog.php');

/************************************************

【 id別アクセスカウンタ 】

※パラメータ[id]の規則
・1文字以上200文字以内であること
・先頭と末尾の空白は削除される
・IdsFilePathに存在する文字列であること

※csvデータの符号化
・改行 　　　　　　　→ <<crlf>>
・カンマ(,)　　　　　→ <<comma>>
・ダブルクォート(")　→ <<quot>>

************************************************/

$config = new Config();
$acslog = new AccessLog();
$result = false;

$reqId = isset($_GET["id"]) ? trim($_GET["id"]) : "";

$dataFolderPath = $config->DataFolderPath;
$idsFilePath = $config->IdsFilePath;
$logFolderPath = $config->LogFolderPath;
$logFilePrefix = "";

if ( $reqId <> "" )
{
	if ( $acslog->IsGsiId($reqId, $idsFilePath) )
	{
		$acslog->SaveLog($logFolderPath, $logFilePrefix, $reqId);
		$result = true;
	}
}

echo $result ? "1" : "0";


?>