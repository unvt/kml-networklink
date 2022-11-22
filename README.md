# kml-networklink
Build and update web pages to deliver KML data for displaying maps and aerial photographs viewable on the Geospatial Information Authority of Japan's map, "GSI Maps" on KML network link-compatible software applications. 


## Requirement
- Windows10 / Windows Server 2016 or later
- Apache 2.4
- PHP 8.1
- jQuery 3


## Setting
- Some PHP scripts in this project run as cgi, while others run as commands. To invoke commands, specify the php folder as a PATH system variable (add the PATH to the php folder to environment variables).
- Set `htdocs` as the document root for the website.
- `manager` is the administration tool, and data storage area.  


## Usage
- To update KML data, use the [layers*.txt of GSI Maps] (https://github.com/gsi-cyberjapan/gsimaps/tree/gh-pages/layers_txt).
- Execute the administration tool (manager/GsiKmlNetworkLinkManager.exe) to display the default settings screen.
- Configure folders and URL settings.
- In the [Create KML Data] tab, click the [Update Web Page] button to update the web page and KML data.


## Licence
MIT


## Author
[The United Nations Vector Tile Toolkit](https://github.com/unvt)

