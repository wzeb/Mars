<%@ page language="java"  import="java.util.*" pageEncoding="UTF-8"%>
<script>
$('#ff').layout("panel","west").panel("setTitle","导航盘");

$('#westtree').tree({
	onClick: function(node){
// 		console.log(node);
		switch(node.text){
		case "IPS日志":
			LoadRawContent("IPS");
			return ;
		case "僵尸网络日志":
			LoadRawContent("ZombieNetwork");
			return ;
		case "应用服务控制日志":
			LoadRawContent("SAC");
			return ;
		default:
			return;
		}
	}
})

function LoadRawContent(content){
	$('#panel_center').layout("panel","center").panel("refresh","jsp/center/raw/"+content+"raw.jsp");
	$('#panel_center').layout("panel","north").panel("refresh","jsp/center/raw/"+content+"figure.jsp");
	return ;
}

</script>

<ul id="westtree" class="easyui-tree">
	<li>首页</li>
    <li>
		<span>源文件</span>
		<ul>
			<li>
				<span>来源南京实验室</span>
				<ul>
					<li><span>IPS日志</span></li>
					<li><span>应用服务控制日志</span></li>
					<li><span>僵尸网络日志</span></li>
				</ul>
				
			</li>
			<li><span>日志来源2</span></li>
			<li><span>日志来源3</span></li>
		</ul>
	</li>
    <li><span>分析</span>
		<ul>
			<li><span>日志来源1</span></li>
			<li><span>日志来源2</span></li>
			<li><span>日志来源3</span></li>
		</ul>
	</li>
</ul>

