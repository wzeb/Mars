package ac.ict.menelaus.aiops.controller;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import ac.ict.menelaus.aiops.object.vo.ResponseVo;
import ac.ict.menelaus.aiops.service.intf.IIPSService;
import ac.ict.menelaus.aiops.utils.WebUtils;

@Controller("IPS")
public class IPSController {
    private static final Logger LOG = LoggerFactory.getLogger(IPSController.class);

	@Resource
	private IIPSService iPSService;

	@RequestMapping(value = "pageview")
    @ResponseBody
    public ResponseVo Index(@RequestParam Integer count, @RequestParam Integer offset) {
    	LOG.debug("Get");
        return WebUtils.Response(iPSService.showPage(offset, count));
    }
	
	
}
