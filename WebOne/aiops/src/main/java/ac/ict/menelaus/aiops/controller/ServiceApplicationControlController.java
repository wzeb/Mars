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

import ac.ict.menelaus.aiops.object.dao.ServiceApplicationControl;
import ac.ict.menelaus.aiops.object.vo.ResponseVo;
import ac.ict.menelaus.aiops.service.intf.IServiceApplicationControlService;
import ac.ict.menelaus.aiops.utils.WebUtils;

@Controller
@RequestMapping("SAC")
public class ServiceApplicationControlController {


    private static final Logger LOG = LoggerFactory.getLogger(ServiceApplicationControlController.class);

	@Resource
	private IServiceApplicationControlService serviceApplicationControlService;

	@RequestMapping(value = "pageview.do", method = RequestMethod.POST)
    @ResponseBody
    public ResponseVo Index(@RequestParam(required = false) String StartDate,
    		@RequestParam(required = false) String EndDate) {
    	LOG.debug("Get: StartDate="+StartDate+", EndDate="+EndDate);
    	List<ServiceApplicationControl> kList = serviceApplicationControlService.showPage(StartDate, EndDate);
        return WebUtils.Response(kList);
    }
}
