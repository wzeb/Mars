package ac.ict.menelaus.aiops.controller;

import java.util.List;

import javax.annotation.Resource;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import ac.ict.menelaus.aiops.object.dao.IPS;
import ac.ict.menelaus.aiops.object.vo.ResponseVo;
import ac.ict.menelaus.aiops.service.intf.IIPSService;
import ac.ict.menelaus.aiops.utils.WebUtils;

@Controller
@RequestMapping("IPS")
public class IPSController {
    private static final Logger LOG = LoggerFactory.getLogger(IPSController.class);

	@Resource
	private IIPSService iPSService;

	@RequestMapping(value = "pageview.do", method = RequestMethod.POST)
    @ResponseBody
    public ResponseVo Index(@RequestParam Integer count, @RequestParam Integer offset) {
    	LOG.debug("Get: count="+count+", offset="+offset);
    	List<IPS> kList = iPSService.showPage(offset, count);
        return WebUtils.Response(kList);
    }
	
	
}
