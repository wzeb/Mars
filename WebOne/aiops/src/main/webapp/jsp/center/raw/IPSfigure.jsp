<%@ page language="java"  import="java.util.*" pageEncoding="UTF-8"%>


<script>

var chart;
function newRender(newValue, oldValue){
	var data = $('#IPSRaw').datagrid('getData');
	var aggregate_target = $('#aggregate_target').combobox('getValue');
	var chart_type = $('#chart_type').combobox('getValue');
	var point = $('#interval_unit').combobox('getValue');
 	var getTime = tar => roundTime(tar["timestamp"],point).getTime();	
	var aggredata = cummulation(getTime,aggregate_target,data.rows);	
	var series = cummToApexCharts(aggredata);

	console.log(series);
	
// 	series = [{name:"默认",data: [{ x: '05/06/2014', y: 54 }, { x: '05/08/2014', y: 17 } ,  { x: '05/28/2014', y: 26 }]}];
// 	console.log(series);
	
	if(chart != null){
		chart.destroy();
	}
	var options = {
		chart: {
			width: '100%',
			height: '90%',
		    type: chart_type,
// 			zoom: {
// 			    type: 'x',
// 			    enabled: true
// 			  },
		},
		series: series
	};
	chart = new ApexCharts(document.querySelector("#ipschart"), options);
	chart.render();
	//chart.updateOptions({series:series});
}
</script>

<div>时间积累：
<!-- 	<input id="time_interval" class="easyui-numberspinner" style="width:65px" -->
<!-- 	required="required" data-options="min:0,max:99,editable:true,onChange:newRender" value="1"> -->
	<select id="interval_unit" class="easyui-combobox" name="interval_unit" 
	style="width:60px;" value="min" data-options="editable:false,onChange:newRender">
		<option value="sec">秒</option>
		<option value="min">分</option>
		<option value="hr" selected="selected">时</option>
		<option value="day">日</option>
		<option value="mon">月</option>
		<option value="yr">年</option>
	</select>
	积累对象：
	<select id="aggregate_target" class="easyui-combobox" name="aggregate_target" 
	style="width:130px;" value="min" data-options="editable:false,onChange:newRender">
		<option value="ruleName" selected="selected">策略名称</option>
		<option value="vulnerabilityId">漏洞ID</option>
		<option value="vulnerabilityName">漏洞名称</option>
		<option value="sourceIp">源IP</option>
		<option value="sourcePort">源端口</option>
		<option value="destinationIp">目的IP</option>
		<option value="destinationPort">目的端口</option>
		<option value="protocol">协议</option>
		<option value="attackType">攻击类型</option>
		<option value="severity">严重等级</option>
		<option value="systemAction">动作</option>
	</select>
	样式：
	<select id="chart_type" class="easyui-combobox" name="chart_type" 
	style="width:130px;" value="min" data-options="editable:false,onChange:newRender">
		<option value="line" selected="selected">线性图</option>
		<option value="bar">直方图</option>
		<option value="area">区域图</option>
	</select>
</div>
<div id="ipschart">
</div>