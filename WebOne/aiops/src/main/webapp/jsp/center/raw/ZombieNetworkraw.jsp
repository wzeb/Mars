<%@ page language="java"  import="java.util.*" pageEncoding="UTF-8"%>
<script>
$('#ZombieNetworkRaw').datagrid({
	rownumbers:true,
	singleSelect:true,
	toolbar:'#tb',
    columns:[[
		{field:'id',title:'ID',width:45},
		{field:'timestamp',title:'时间',width:150,
			formatter: function(value,row,index){
				return dateFormat("yyyy-MM-dd hh:mm:ss",new Date(value));
			}},
		{field:'ruleName',title:'策略名称',width:120},
		{field:'featureId',title:'特征ID',width:100},
		{field:'sourceIp',title:'源IP',width:150},
		{field:'sourcePort',title:'源端口',width:100},
		{field:'destinationIp',title:'目的IP',width:150},
		{field:'destinationPort',title:'目的端口',width:100},
		{field:'attackType',title:'攻击类型',width:150},
		{field:'severity',title:'严重等级',width:100},
		{field:'systemAction',title:'动作',width:100}
    ]]
});

$(function(){
    $('#submit').bind('click', function(){
    	var params = {};
		var startDate = $('#startDate').datetimebox('getValue');
		var endDate = $('#endDate').datetimebox('getValue');
		if(startDate){
			params["StartDate"] = startDate;
		}
		if(endDate){
			params["EndDate"] = endDate;
		}
		$.ajax({
			url:'${rootPath}/aiops/ZombieNetwork/pageview.do',
			type:"POST",
			dataType:"json",
			data: params,
			success:function(result){
				if(result.code){
					console.log(result.message)
				}
				else{
					$('#ZombieNetworkRaw').datagrid('loadData',result.data);
					var data = result.data;
					newRender();
				}
			}
		})
    });
});
</script>


<table id="ZombieNetworkRaw" class="easyui-datagrid" style="width:100%;height:100%;">
<!-- 	data-options="rownumbers:true,singleSelect:true,toolbar:'#tb',fitColumns:true"> -->

</table>
<div id="tb" style="padding:5px;height:auto">
	<div>
		开始日期: <input id="startDate" class="easyui-datetimebox" style="width:180px">
		<a id="clearStartDate" class="easyui-linkbutton" iconCls="icon-clear" onclick="$('#startDate').datetimebox('clear')"></a>
		结束日期: <input id="endDate" class="easyui-datetimebox" style="width:180px">
		<a id="clearEndDate" class="easyui-linkbutton" iconCls="icon-clear" onclick="$('#endDate').datetimebox('clear')"></a>
		<a id="submit" href="#" class="easyui-linkbutton" iconCls="icon-search">查询</a>
	</div>
</div>