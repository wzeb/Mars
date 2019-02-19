package ac.ict.menelaus.aiops.dal.impl;

import java.util.List;

import javax.annotation.Resource;

import org.mybatis.spring.SqlSessionTemplate;
import org.springframework.stereotype.Repository;

import ac.ict.menelaus.aiops.dal.intf.ITestDal;
import ac.ict.menelaus.aiops.object.dao.Test;

@Repository
public class TestDal implements ITestDal{

    @Resource
    private SqlSessionTemplate sqlSessionTemplate;

	public List<Test> showAll() {
		return sqlSessionTemplate.selectList(this.getClass().getName() + ".showAll");
	}
    
}
