package ac.ict.menelaus.aiops.service.impl;

import java.util.ArrayList;
import java.util.List;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import ac.ict.menelaus.aiops.dal.intf.ITestDal;
import ac.ict.menelaus.aiops.service.intf.ITestService;
import ac.ict.menelaus.aiops.object.dao.Test;

@Service
public class TestService implements ITestService{

	@Resource
	private ITestDal testDal;

	public List<List<String> > ShowAll() {
		List<Test> alldata = testDal.showAll();
		List<List<String> > shows = new ArrayList<List<String>>();
		for (Test test : alldata) {
			List<String> line = new ArrayList<String>();
			line.add(test.getId() + "");
			line.add(test.getName());
			line.add(test.getSex());
			line.add(test.getAge() + "");
			line.add(test.getAddress());
			line.add(test.getEmail());
			shows.add(line);
		}
		return shows;
	}
	
	
}
