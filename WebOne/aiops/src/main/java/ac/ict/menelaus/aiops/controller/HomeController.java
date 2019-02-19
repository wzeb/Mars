package ac.ict.menelaus.aiops.controller;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import ac.ict.menelaus.aiops.service.intf.ITestService;

@Controller("home")
public class HomeController {
    private static final Logger LOG = LoggerFactory.getLogger(HomeController.class);

    @Resource
    private ITestService testService;
    
    @RequestMapping(value = "index.do")
    public String Index() {
    	LOG.debug("INTO");
        return "index";
    }

    
    
}