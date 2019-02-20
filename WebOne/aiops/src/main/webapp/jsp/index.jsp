<%@ page language="java" import="java.util.*" pageEncoding="utf-8" %>
<link rel="stylesheet" type="text/css" href="jsp/lib/easyui/themes/default/easyui.css">
<link rel="stylesheet" type="text/css" href="jsp/lib/easyui/themes/icon.css">
<script type="text/javascript" src="jsp/lib/easyui/jquery.min.js"></script>
<script type="text/javascript" src="jsp/lib/easyui/jquery.easyui.min.js"></script>
<script type="text/javascript" src="jsp/lib/easyui/locale/easyui-lang-zh_CN.js"></script>

<html>
<body id="ff" class="easyui-layout">
     <div data-options="region:'north',href:'jsp/top.jsp'" style="height:100px;"></div>
     <div data-options="region:'west',title:'west',split:true,href:'jsp/west.jsp'" style="width:220px;"></div>
     <div data-options="region:'center',title:'数据区',href:'jsp/center.jsp'" style="padding:5px;"></div>  
  </body> 
</html>
