package ac.ict.menelaus.aiops.controller;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller("home")
public class HomeController {
    private static final Logger LOG = LoggerFactory.getLogger(HomeController.class);

    @RequestMapping(value = "index.do")
    public String Index() {
    	LOG.debug("INTO");
        return "index";
    }

}