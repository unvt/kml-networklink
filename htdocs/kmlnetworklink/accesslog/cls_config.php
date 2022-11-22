<?php
ini_set("display_errors", 0);
ini_set("error_reporting", E_ALL);
ini_set("date.timezone", "Asia/Tokyo");
ini_set("default_chaset", "UTF-8");



class Config
{
	public $DataFolderPath;
	public $IdsFilePath;
	public $LogFolderPath;
	
	public function __construct()
	{
		$this->DataFolderPath = '../../../manager/data';
		$this->IdsFilePath    = $this->DataFolderPath . '/layers_ids.txt';
		$this->LogFolderPath  = $this->DataFolderPath . '/log';
	}
}


?>