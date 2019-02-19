package ac.ict.menelaus.aiops.service.impl;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import ac.ict.menelaus.aiops.dal.intf.ITestDal;
import ac.ict.menelaus.aiops.service.intf.ITestService;

@Service
public class TestService implements ITestService{

	@Resource
	private ITestDal testDal;
	
	
}
