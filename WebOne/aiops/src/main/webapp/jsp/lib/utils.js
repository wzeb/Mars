/**************************************时间格式化处理************************************/

function dateFormat(fmt,date)   
{ //author: meizz   
  var o = {   
    "M+" : date.getMonth()+1,                 //月份   
    "d+" : date.getDate(),                    //日   
    "h+" : date.getHours(),                   //小时   
    "m+" : date.getMinutes(),                 //分   
    "s+" : date.getSeconds(),                 //秒   
    "q+" : Math.floor((date.getMonth()+3)/3), //季度   
    "S"  : date.getMilliseconds()             //毫秒   
  };   
  if(/(y+)/.test(fmt))   
    fmt=fmt.replace(RegExp.$1, (date.getFullYear()+"").substr(4 - RegExp.$1.length));   
  for(var k in o)   
    if(new RegExp("("+ k +")").test(fmt))   
  fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));   
  return fmt;   
}

function roundTime(time,point)
{
	var date = new Date(time);
	switch(point){
	case "yr":
		date.setMonth(0);
	case "mon":
		date.setDate(1);
	case "day":
		date.setHours(0);
	case "hr":
		date.setMinutes(0);
	case "min":
		date.setSeconds(0);
		return date;
	}
};

const cummulation = (tarfun , aggre , array) =>
	array.reduce((objectsByKeyValue,obj)=>{
		const key = obj[aggre];
		const xAxis = tarfun(obj);
		if(objectsByKeyValue[key]){
			if(xAxis in objectsByKeyValue[key]){
				objectsByKeyValue[key][xAxis] += 1;
			}
			else{
				objectsByKeyValue[key][xAxis] = 1;
			}
		}
		else{
			objectsByKeyValue[key] = {};
			objectsByKeyValue[key][xAxis] = 1;
		}
		return objectsByKeyValue;
	}
	,{});
	
function cummToApexCharts(chartData){
	var series = [];
	for(var x in chartData){
		var data = [];
		for(var key in chartData[x]){
			var element = {
					x:dateFormat("yyyy-MM-dd hh:mm:ss",new Date(parseInt(key))),
					y:chartData[x][key]
			};
			
			data.push(element);
		}
		var sery = {};
		sery["data"] = data;
		sery["name"] = x;
		series.push(sery);
	}
	return series;
}