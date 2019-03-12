<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<body id="ff" class="easyui-layout">
     <div data-options="region:'north',href:'jsp/top.jsp'" style="height:100px;"></div>
     <div data-options="region:'center',title:'west',split:true,href:'jsp/west.jsp'" style="width:220px;"></div>
     <div data-options="region:'south',title:'数据区',href:'jsp/center.jsp'" style="padding:5px;"></div>  
  </body> 
</html>