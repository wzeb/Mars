<%@ page language="java"  import="java.util.*" pageEncoding="UTF-8"%>
<script>
$('#ff').layout("panel","west").panel("setTitle","导航盘");

$('#westtree').tree({
	onClick: function(node){
// 		console.log(node);
		switch(node.text){
		case "IPS日志":
			$('#ff').layout("panel","center").panel("refresh","jsp/center.jsp");
			console.log("hit");
			return ;
		default:
			return;
		}
	}
})

function LoadContent(content){
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

