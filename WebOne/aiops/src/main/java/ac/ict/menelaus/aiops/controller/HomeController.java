package ac.ict.menelaus.aiops.controller;

import java.util.List;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import ac.ict.menelaus.aiops.service.intf.ITestService;

@Controller("home")
public class HomeController {
    private static final Logger LOG = LoggerFactory.getLogger(HomeController.class);

    @Resource
    private ITestService testService;
    
    @RequestMapping(value = "index.do")
    @ResponseBody
    public String Index() {
    	LOG.debug("INTO");
    	List<List<String>> list = testService.ShowAll();
    	for (List<String> line : list) {
    		String content = "";
			for (String string : line) {
				content += string + " ";
			}
			LOG.debug(content);
		}
        return "index";
    }

    
    
}