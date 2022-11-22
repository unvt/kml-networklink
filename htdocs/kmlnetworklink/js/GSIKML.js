var GSIKML = {
	pankuzuDelimiter: "#dlmt#",	
	loaded: false,
	data: [],
	
	init: function()
	{
		$("#dataList .layerGroup").each(function(){
			var pankuzuArr = [];
			
			$(this).find(".tag").each(function(){
				pankuzuArr.push( $(this).text() );
				$(this).on("click", function(){
					$("#kmlsearch").val( $(this).text() );
					GSIKML.search();
				});
			});
			
			var pankuzu = pankuzuArr.join(GSIKML.pankuzuDelimiter);
			
			$(this).find(".layer").each(function(){
				var layer = [];
				layer["id"] = $(this).attr("id");
				layer["title"] = $(this).find(".layerName").text();
				layer["desc"] = $(this).find(".layerDesc").html();
				layer["pankuzu"] = pankuzu;
				GSIKML.data.push(layer);
				//if ( GSIKML.data.length % 100 == 0 ) console.log(GSIKML.data[GSIKML.data.length-1]); 
			});
		});
		this.loaded = true;
		this.onDataLoad();
		
		$("#kmlsearch").on("keyup", function(){
			GSIKML.search();
		});
		
		$("#kmlsearchClear").on("click", function(){
			$("#kmlsearch").val("");
			GSIKML.search();
		});

		$("#tabs .tab").on("click", function(){
			$("#tabs .tab").removeClass("active");
			$(this).addClass("active");
			GSIKML.search();
		});

	},
	
	search: function()
	{
		if ( !this.loaded )
		{
			setTimeout(GSIKML.search, 100);
			return;
		}
		
		searchString = $("#kmlsearch").val();
		if ( this.running )
		{
			this.canceled = true; 
			if ( this.waitTimerId )
			{
				clearTimeout(this.waitTimerId);
			}
			this.waitTimerId = setTimeout(function(){
				this.search();
			}.bind(this), 10);
			return;
		}
		
		this.start = Date.now();
		this.tables = [];
		this.canceled = false;
		this.running = true;
		$("#loading").css({'visibility':'visible'});
		$("#dataList").html("");
		
		setTimeout(this.searchToBuild.bind(this, searchString), 100);
	},
	
	searchToBuild: function( searchString )
	{
		var s = searchString;
		if ( searchString )
		{
			s = s.replace(/\$/, '\\$');
			s = s.replace(/\(/, '\\(');
			s = s.replace(/\)/, '\\)');
			s = s.replace(/\-/, '\\-');
			s = s.replace(/\^/, '\\^');
			s = s.replace(/\\/, '\\\\');
			s = s.replace(/\|/, '\\|');
			s = s.replace(/\{/, '\\{');
			s = s.replace(/\}/, '\\}');
			s = s.replace(/\[/, '\\[');
			s = s.replace(/\]/, '\\]');
			s = s.replace(/\+/, '\\+');
			s = s.replace(/\*/, '\\*');
			s = s.replace(/\./, '\\.');
			s = s.replace(/\?/, '\\?');
			s = s.replace(/\//, '\\/');
		}
		
		var tabString = $("#tabs .active").text();
		var tabReg = new RegExp('^' + tabString, "i");
		
		var reg = new RegExp(s, "i");
		var cnt = 0;
		var bf = "";
		
		for ( var i=0; i<this.data.length; i++ ){
			if ( this.canceled )
			{
				this.canceled = false;
				this.tables = [];
				break;
			}
			
			// タブ判定
			if ( !$("#tabs .active").hasClass("all") && !this.data[i]["pankuzu"].match(tabReg) ) continue;
			var layerId = this.data[i]["id"];
			var filename = "gsi_" + layerId + ".kml";
			var searchTarget = this.data[i]["pankuzu"] + "#dlmt#" + this.data[i]["title"] + "#dlmt#" + filename;
			if ( searchString != "" && !searchTarget.match(reg) ) continue;
			
			cnt++;
			var url = location.protocol + "//" + location.hostname + "/kmlnetworklink/kml/" + filename;
			
			if ( this.data[i]["pankuzu"] != bf ) {
				bf = this.data[i]["pankuzu"];
				
				pankuzu = this.data[i]["pankuzu"].split('#dlmt#');
				for ( var j=0; j<pankuzu.length; j++ )
				{
					pankuzu[j] = '<span class="tag">' + pankuzu[j] + '</span>';
				}
				
				var table = $("<table></table>")
					.attr("border", 0);
				
				var trHead = $("<tr></tr>")
					.appendTo( table );
					
				var tdHead = $("<td></td>")
					.attr("colspan", 3)
					.html( pankuzu.join("") )
					.appendTo( trHead );
					
				table.append("<tr><th>地図、空中写真の種類</th><th>説明</th><th>直接URLを指定する場合は、こちらのURLを指定してください</th></tr>");
				
				this.tables.push(table);
								

			}

			var tr = $("<tr></tr>")
				.appendTo( table );
			
			//var td1 = $("<td></td>")
			//	.html(i)
			//	.appendTo( tr );
				
			var td2 = $("<td></td>")
				.html( this.data[i]["title"] )
				
				.appendTo( tr );
				
			var td4 = $("<td></td>")
				.html( this.data[i]["desc"] )
				.appendTo( tr );
				
			var td3 = $("<td></td>")
				.appendTo( tr );
				
			var a = $("<a></a>")
				.html(url)
				.attr("href", url)
				.attr("layerId", layerId)
				.on("click", function(){
					$.get('accesslog/index.php',{
							id: $(this).attr("layerId")
						}
					);
				})
				.appendTo( td3 );
		}
		
		$("#displayCount").text(cnt);
		this.appendTable();
		
		
		
		
	},
	appendTable : function()
	{
		if ( this.canceled )
		{
			this.canceled = false;
			this.tables = [];
		}
		
		if ( this.tables.length > 0 )
		{
			var table = this.tables.shift();
			$("#dataList").append(table);
			$(table).find(".tag").on("click", function(){
				$("#kmlsearch").val( $(this).text() );
				GSIKML.search();
			});
			setTimeout(function(){
				this.appendTable();
			}.bind(this), 1);
		}
		else
		{
			$("#loading").css({'visibility':'hidden'});
			this.running = false;
		}
	},
	
	access: function( elm )
	{
		$.get('accesslog/index.php',{
				id: $(elm).attr("layerId")
			}
		);
	},
	
	onDataLoad: function() {
	}
};


GSIKML.onDataLoad = function(){
};


