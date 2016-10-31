/*##########################################################################
 * 
 * GoogleMap.js
 * -------------------------------------------------------------------------
 * By
 * Murat Firat, April 2010
 * bmfirat@gmail.com
 * 
 * -------------------------------------------------------------------------
 * Description:
 * 
 * Implements client side methods required for google map control
 * 
 * -------------------------------------------------------------------------
 ###########################################################################*/

    var map;

    function Initialize()
    {                   
        var latitude,longtitude, zoomlevel, maptype, maptypeid_;
                
        var mapInfo=document.getElementById('hdMapInfo').value;
                
        var paramArray=mapInfo.split('|');
        for(var i=0;i<paramArray.length;i++)
        {
            var tmp=paramArray[i];
            var param=tmp.split(':');
            if(param.length==2)
            {
                var paramCode=param[0];
                var paramValue=param[1];
                
                switch(paramCode.toLowerCase())
                {
                    case 'lat':
                    latitude=param[1];
                    break;
                    
                    case 'lng':
                    longtitude=param[1];
                    break;
                    
                    case 'zoom':
                    zoomlevel=parseInt(param[1]);
                    break;
                    
                    case 'maptype':
                    maptype=param[1];
                    break;
                    
                    default:
                    break;                    
                }
            }
        }               
                
        maptypeid_=google.maps.MapTypeId.ROADMAP;//default
        if(maptype.toUpperCase()=='ROADMAP')
            maptypeid_=google.maps.MapTypeId.ROADMAP;
        else if(maptype.toUpperCase()=='SATELLITE')
            maptypeid_=google.maps.MapTypeId.SATELLITE;
        else if(maptype.toUpperCase()=='HYBRID')
            maptypeid_=google.maps.MapTypeId.HYBRID;
        else if(maptype.toUpperCase()=='TERRAIN')
            maptypeid_=google.maps.MapTypeId.TERRAIN;
        
        var centerLatlng = new google.maps.LatLng(latitude,longtitude);
                                             
        var myOptions = {
            zoom: zoomlevel,
            center: centerLatlng,
            mapTypeId: maptypeid_,
            scrollwheel: true
        }
        
         map = new google.maps.Map(document.getElementById('map_canvas'), myOptions);
                                   
         google.maps.event.addListener(map, 'click', function(event) {
            MapClicked(event.latLng);
         });
                                            
         google.maps.event.addListener(map, 'zoom_changed', function() {
            ZoomChanged();
         });
                      
         google.maps.event.addListener(map, 'maptypeid_changed', function() {
            MapTypeChanged();
         });
         
         google.maps.event.addListener(map, 'center_changed', function() {
            MapCenterChanged();
         });
         
         google.maps.event.addListener(map, 'dragstart', function() {
            DragStarted();
         });
         
         google.maps.event.addListener(map, 'dragend', function() {
            DragEnded();
         });      
         
         PlaceMarkers();
         
         if(document.getElementById('hdRouteMapInfo').value&&document.getElementById('hdRouteMapInfo').value!='')
            PlayRouteMap(document.getElementById('hdRouteMapInfo').value);
           
    }    

    var draggingNow=false;
    function DragStarted()
    {
        draggingNow=true;
    }
    
    function DragEnded()
    {
        draggingNow=false;
        MapCenterChanged();
    }
     
    function MapCenterChanged()
    {   
        //while dragging, center changed event fires, which is not necessary
        if(!draggingNow)
        {           
            RefreshMapInfo_DoCallBack('MapCenterChanged','','','');               
        }
    }
    
    function ZoomChanged()
    {
        RefreshMapInfo_DoCallBack('ZoomChanged','','','');
    }
    
    function MapTypeChanged()
    {
        RefreshMapInfo_DoCallBack('MapTypeChanged','','','');
    }
    
    function MapClicked(location)
    {
        RefreshMapInfo_DoCallBack('MapClicked', location.lat(), location.lng(), '');
    }
    
    function RefreshMapInfo_DoCallBack(eventTypeId, extra1, extra2, extra3)
    {        
        var mapInfo='lat:'+map.getCenter().lat()+'|lng:'+map.getCenter().lng()+'|zoom:'+map.getZoom()+'|maptype:'+map.getMapTypeId();
        if(extra1!='') mapInfo=mapInfo+'|extra1:'+extra1;
        if(extra2!='') mapInfo=mapInfo+'|extra2:'+extra2;
        if(extra3!='') mapInfo=mapInfo+'|extra3:'+extra3;
                        
        var markersInfo=MarkersListToString();
                        
        document.getElementById('hdMapInfo').value=mapInfo;
        document.getElementById('hdMarkers').value=markersInfo;
        
        var attachedEvents=document.getElementById('hdAttachedEvents').value;
        
        if(attachedEvents.search(eventTypeId)!=-1){       
            //var clientArg= 'eventtype:'+eventTypeId+'|'+mapInfo;
            var clientArg= 'eventtype:'+eventTypeId+'<hdMapInfo>'+mapInfo+'</hdMapInfo>'+'<hdMarkers>'+markersInfo+'</hdMarkers>';
            
            DoCallBack(clientArg, HandleCallBackResponse,'bos gecebiliriz..',null)
        }
    }      
       
    function HandleCallBackResponse(arg)
    {    
        var latitude,longtitude, zoomlevel, maptype, maptypeid_, markerprop;
                
        var mapInfo=document.getElementById('hdMapInfo').value;
                        
        var paramArray=arg.split('||');
        for(var i=0;i<paramArray.length;i++)
        {
            var tmp=paramArray[i];
            var param=tmp.split('####');
            if(param.length==2)
            {
                var paramCode=param[0];
                var paramValue=param[1];
                                
                switch(paramCode.toLowerCase())
                {
                    case 'setcenter':
                        var tmp2=param[1].split(',,');
                        if(tmp2.length==2)
                        {
                            latitude=tmp2[0];
                            longtitude=tmp2[1];  
                            var centerLatlng = new google.maps.LatLng(latitude,longtitude);
                            map.setCenter(centerLatlng);                   
                        }
                    break;
                    
                    case 'setzoom':
                        zoomlevel=parseInt(param[1]);
                        map.setZoom(zoomlevel);
                    break;
                    
                    case 'setmaptype':
                        maptype=param[1];
                        maptypeid_=google.maps.MapTypeId.ROADMAP;//default
                        if(maptype.toUpperCase()=='ROADMAP')
                            maptypeid_=google.maps.MapTypeId.ROADMAP;
                        else if(maptype.toUpperCase()=='SATELLITE')
                            maptypeid_=google.maps.MapTypeId.SATELLITE;
                        else if(maptype.toUpperCase()=='HYBRID')
                            maptypeid_=google.maps.MapTypeId.HYBRID;
                        else if(maptype.toUpperCase()=='TERRAIN')
                            maptypeid_=google.maps.MapTypeId.TERRAIN;
                        map.setMapTypeId(maptypeid_);
                    break;
                             
                    case 'addmarker':
                        markerprop=param[1];
                        PlaceMarker(markerprop);                        
                    break;
                        
                    case 'updatemarker':
                        markerprop=param[1];
                        UpdateMarker(markerprop);                                                
                    break;
                    
                    case 'playroutemap':
                        //markerprop=param[1];
                        PlayRouteMap(param[1]);                                                
                    break;
                                        
                    default:
                    break;                    
                }
            }
        }                   
        //alert('HandleCallBackResponse.. server response: '+arg);
    }
    
    var MarkersList;
    var MarkersListIndex;
    //lat::29.444,,lng::40.11,,imgsrc::http://ghghghh,,imgw::12,,imgh::12||lat::29.33,,lng::40.41,,imgsrc::http://dfdf,imgw:12,imgh:12
    function PlaceMarkers()
    {        
        MarkersList=new Array();
        MarkersListIndex=new Array();
        
        var markersStr=document.getElementById('hdMarkers').value;
        var markersArr=markersStr.split('||');
                
        for(var i=0;i<markersArr.length;i++)
        {
            var markerStr=markersArr[i];
            PlaceMarker(markerStr);
        }        
    }
    
    function PlaceMarker(markerStr)
    {
        var markerInfo=MarkerFromString(markerStr);  
            
        if(markerInfo==null)return;
            
        var markerlatlng=new google.maps.LatLng(markerInfo.lat, markerInfo.lng);
                    
        var marker = new google.maps.Marker({
            position: markerlatlng, 
            map: map,
            visible:markerInfo.visible
        });
                        
        if(markerInfo.imgsrc!=null)
        {
            var imgsize= (markerInfo.imgh!=null&&markerInfo.imgw!=null)?new google.maps.Size(markerInfo.imgw,markerInfo.imgh):null;
            var img=new google.maps.MarkerImage(markerInfo.imgsrc,null,null,null,imgsize);
            marker.setIcon(img);
        }   
        
        if(markerInfo.tooltip!=null)
        {
            marker.setTitle(markerInfo.tooltip);
        }
        
        if(markerInfo.draggable)
        {
            marker.setDraggable(markerInfo.draggable);
            google.maps.event.addListener(marker, 'dragend', function(event) {
                document.getElementById('hdMarkers').value=MarkersListToString();
            });
            
        }
                
        
        if(markerInfo.infoWindowContentHtml&&markerInfo.infoWindowContentHtml!='')
        {        
             google.maps.event.addListener(marker, 'click', function() {
                 
                 if(markerInfo.infoWindowContentHtml&&markerInfo.infoWindowContentHtml!='')//in case of ajax updates
                 {                 
 /*
 if(markerInfo.infoWindowIsOpen==true) alert('markerInfo.infoWindowIsOpen is true'); 
 else alert('markerInfo.infoWindowIsOpen is false');
 
 if(markerInfo.infoWindow) alert('markerInfo.infoWindow is defined'); else alert('markerInfo.infoWindow is null');
 */
                    if(markerInfo.infoWindowIsOpen==true&&markerInfo.infoWindow)
                    {
                        //infoWindow is open, now close it on the second click..
                        markerInfo.infoWindow.close();
                        markerInfo.infoWindowIsOpen=false;
                    }
                    else if(markerInfo.infoWindow)
                    {
                        //infoWindow is closed, now open it on the second click..
                        markerInfo.infoWindow.setContent(markerInfo.infoWindowContentHtml);
                        markerInfo.infoWindow.open(map,marker);
                        markerInfo.infoWindowIsOpen=true;
                    }
                    else
                    {   
                        //infoWindow does not exist, create and open it..                 
                        var infWindow= new google.maps.InfoWindow({
                            content:markerInfo.infoWindowContentHtml,
                            position:marker.getPosition()
                        });   
                        
                        infWindow.open(map,marker);      
                        markerInfo.infoWindow=infWindow;
                        markerInfo.infoWindowIsOpen=true;
                    }
                 }                
             });  
                        
        }       
        
        markerInfo.marker=marker;
        
        MarkersList[markerInfo.markerid]=markerInfo;
        MarkersListIndex.push(markerInfo.markerid);
                
        document.getElementById('hdMarkers').value=MarkersListToString();
    }
    
    function UpdateMarker(markerStr)
    {
    //alert(markerStr);
        var markerInfo=MarkerFromString(markerStr);  
            
        if(markerInfo==null)return;
        
        if(!MarkersList[markerInfo.markerid].marker)return;
        
        var currentMarker= MarkersList[markerInfo.markerid].marker ;
        
        if(currentMarker.getPosition().lat()!=markerInfo.lat || currentMarker.getPosition().lng()!=markerInfo.lng)
        {
           currentMarker.setPosition(new google.maps.LatLng(markerInfo.lat, markerInfo.lng)) ;
           MarkersList[markerInfo.markerid].lat=markerInfo.lat;
           MarkersList[markerInfo.markerid].lng=markerInfo.lng;
        }
           
        if(currentMarker.getTitle()!=markerInfo.tooltip)
        {
           currentMarker.setTitle(markerInfo.tooltip);
           MarkersList[markerInfo.markerid].tooltip=markerInfo.tooltip;
        }
               
        if(currentMarker.getVisible()!=markerInfo.visible)
        {
            currentMarker.setVisible( markerInfo.visible );
            MarkersList[markerInfo.markerid].visible=markerInfo.visible;
        }
        
        if(currentMarker.getDraggable()!=markerInfo.draggable)
        {
            currentMarker.setDraggable( markerInfo.draggable );            
            MarkersList[markerInfo.markerid].draggable=markerInfo.draggable;
                        
            if(markerInfo.draggable)
            {
                google.maps.event.addListener(currentMarker, 'dragend', function(event) {
                    document.getElementById('hdMarkers').value=MarkersListToString();
                });                
            }            
        }
        
        if(currentMarker.getIcon()!=markerInfo.imgsrc 
        || MarkersList[markerInfo.markerid].imgh!=markerInfo.imgh 
        || MarkersList[markerInfo.markerid].imgw!=markerInfo.imgw)
        {
            var imgsize= (markerInfo.imgh!=null&&markerInfo.imgw!=null)?new google.maps.Size(markerInfo.imgw,markerInfo.imgh):null;
            var img=new google.maps.MarkerImage(markerInfo.imgsrc,null,null,null,imgsize);
            currentMarker.setIcon(img);
            
            MarkersList[markerInfo.markerid].imgsrc=markerInfo.imgsrc;
            MarkersList[markerInfo.markerid].imgh=markerInfo.imgh;
            MarkersList[markerInfo.markerid].imgw=markerInfo.imgw;
        }
        
        //if(MarkersList[markerInfo.markerid].infoWindowContentHtml!=markerInfo.infoWindowContentHtml)
            MarkersList[markerInfo.markerid].infoWindowContentHtml=markerInfo.infoWindowContentHtml;
            
        document.getElementById('hdMarkers').value=MarkersListToString();
        
    }
    
    /*    
        string routeMapInfo = "positions::";
        foreach (PointF p in Points)
        {
            routeMapInfo += p.X.ToString().Replace(',', '.') + "," + p.Y.ToString().Replace(',', '.') + "-";
        }

        routeMapInfo.Remove(routeMapInfo.Length - 1, 1);
        routeMapInfo += "||moveinterval::" + MoveInterval + "||positionmarker::" + PositionMarker.ToString();
    */
    
    var RouteMapPositionMarker;
    var RouteMapMoveInterval;
    var RouteMapPositions;
    
    function PlayRouteMap(routeMapInfo)
    {           
        //alert(routeMapInfo);
        var routeMapInfoArr = routeMapInfo.split(',,,');   
        
        if(!routeMapInfoArr||routeMapInfoArr=='') return;
        
        if(routeMapInfoArr.length<3)return;
        
        var moveintervalstr=null, positionmarkerstr=null, positonsstr=null;
              
        for(var i=0;i<routeMapInfoArr.length;i++)
        {
            var paramPair= routeMapInfoArr[i].split('==');
            
            if(paramPair.length==2)
            {
                switch(paramPair[0])
                {
                    case "moveinterval":
                    moveintervalstr=paramPair[1];
                    break;
                    
                    case "positionmarker":
                    positionmarkerstr=paramPair[1];
                    break;
                    
                    case "positions":
                    positonsstr=paramPair[1];
                    break;
                    
                    default:
                    break;
                }
            }
        } 
        
        if(!(moveintervalstr&&positionmarkerstr&&positonsstr)) return;
        
        RouteMapMoveInterval = parseInt(moveintervalstr);
        RouteMapPositionMarker= MarkerFromString(positionmarkerstr);
        RouteMapPositions=ParseRouteMapPositions(positonsstr);
               
        //place marker here
        
        RouteMapPositionMarker.lat=RouteMapPositions[0].lat;
        RouteMapPositionMarker.lng=RouteMapPositions[0].lng;
        
        var markerlatlng=new google.maps.LatLng(RouteMapPositionMarker.lat, RouteMapPositionMarker.lng);
                    
        var marker = new google.maps.Marker({
            position: markerlatlng, 
            map: map,
            visible:RouteMapPositionMarker.visible
        });
                        
        if(RouteMapPositionMarker.imgsrc!=null)
        {
            var imgsize= (RouteMapPositionMarker.imgh!=null&&RouteMapPositionMarker.imgw!=null)?new google.maps.Size(RouteMapPositionMarker.imgw,RouteMapPositionMarker.imgh):null;
            var img=new google.maps.MarkerImage(RouteMapPositionMarker.imgsrc,null,null,null,imgsize);
            marker.setIcon(img);
        }
        map.setCenter(markerlatlng);
        
        RouteMapPositionMarker.marker=marker;   
        
        //thread sleep
        PlayRouteMapAsync(0);
    }
    
    
    function PlayRouteMapAsync(index)
    {
        var markerlatlng=new google.maps.LatLng(RouteMapPositions[index].lat, RouteMapPositions[index].lng);
        RouteMapPositionMarker.marker.setPosition(markerlatlng);
        map.setCenter(markerlatlng);
        
        if(index+1<RouteMapPositions.length)
            setTimeout ('PlayRouteMapAsync(' +(index+1)+ ')', RouteMapMoveInterval ); //PlayRouteMapAsync(index+1);
            
    }
    
    function ParseRouteMapPositions(pStr)
    {
        var pArr=new Array();
        
        var pStrArr= pStr.split("-");
        
        for(var i=0;i<pStrArr.length;i++)
        {
            if(pStrArr[i].split(",").length==2)
            {
                var lat=pStrArr[i].split(",")[0];
                var lng=pStrArr[i].split(",")[1];
                pArr.push({ lat:lat,lng:lng });
            }
        }
        
        return pArr;        
    }
    
       
    //   markerid::m1,,lat::40.91,,lng::29.09,,visible::True,,imgsrc::http://yellowicon.com/downloads/images/lin.jpg
    function MarkerFromString(markerStr)
    {
        if(markerStr.length>0)
        {
            var markerid,lat,lng,visible,imgsrc=null,imgh=null,imgw=null, tooltip=null, draggable=null, infoWindowContentHtml=null;
            var paramArr=markerStr.split(',,');      
            
            for(var j=0;j<paramArr.length;j++)    
            {
                var propStr=paramArr[j];
                var prop=propStr.split('::');
                
                if(prop.length!=2)continue;
                
                var propCode=prop[0];
                var propValue=prop[1];
                               
                switch(propCode)
                {
                    case "markerid": markerid=propValue; break;      
                    case "lat": lat=propValue; break;                        
                    case "lng": lng=propValue; break;                                 
                    case "visible": visible=(propValue=="true"); break;                    
                    case "imgsrc": imgsrc=propValue; break;                          
                    case "imgh": imgh=propValue; break;                          
                    case "imgw": imgw=propValue; break;   
                    case "tooltip": tooltip=propValue; break;    
                    case "draggable": draggable=(propValue=="true"); break;
                    case "infoWindowContentHtml": infoWindowContentHtml=propValue; break;
                    default: break;            
                }                    
            } 
                         
            var markerInfo={
                markerid:markerid,
                lat:lat,
                lng:lng,
                imgsrc:imgsrc,
                imgh:imgh,
                imgw:imgw,
                tooltip:tooltip,
                infoWindowContentHtml:infoWindowContentHtml,
                infoWindowIsOpen:false,
                infoWindow:null,
                visible:visible,
                draggable:draggable,
                marker:null
            }
            
            return markerInfo;
        }   
        
        return null;         
    }
    
    
    function MarkersListToString()
    {
        var MarkersListStr="";
        
        for(var i=0;i<MarkersListIndex.length;i++)
        {
        
            var markerInfo=MarkersList[MarkersListIndex[i]];
            
            var markerid=MarkersListIndex[i];
            var lat= markerInfo.marker.getPosition().lat();
            var lng= markerInfo.marker.getPosition().lng();
            var imgsrc=markerInfo.imgsrc;
            var imgh=markerInfo.imgh;
            var imgw=markerInfo.imgw;
            var tooltip= markerInfo.marker.getTitle();
            var infoWindowContentHtml=markerInfo.infoWindowContentHtml;
            var visible= markerInfo.marker.getVisible();
            var draggable=markerInfo.marker.getDraggable();
                        
            MarkersListStr+="markerid::"+MarkersListIndex[i]+",,lat::" +lat+",,lng::"+lng+ ",,visible::" +visible;
                        
            if(tooltip&&tooltip!="")
            {
                MarkersListStr+=",,tooltip::"+tooltip;
            }            
            if(imgsrc&&imgsrc!="")
            {
                MarkersListStr+=",,imgsrc::"+imgsrc; 
            } 
            if(imgh&&imgh!="")
            {
                MarkersListStr+=",,imgh::"+imgh; 
            }
            if(imgw&&imgw!="")
            {
                MarkersListStr+=",,imgw::"+imgw; 
            }
            if(draggable&&draggable!="")
            {
                MarkersListStr+=",,draggable::"+draggable; 
            }
            if(infoWindowContentHtml&&infoWindowContentHtml!="")
            {
                MarkersListStr+=",,infoWindowContentHtml::"+infoWindowContentHtml; 
            }    
            
            MarkersListStr+='||'
        }
        //alert(MarkersListStr);
        
        return MarkersListStr;
        
        
    }
    
        