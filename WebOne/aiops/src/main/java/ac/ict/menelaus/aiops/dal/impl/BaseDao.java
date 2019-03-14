package ac.ict.menelaus.aiops.dal.impl;

import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.mybatis.spring.SqlSessionTemplate;

import ac.ict.menelaus.aiops.dal.intf.IBaseDal;

public class BaseDao<T> implements IBaseDal<T> {
	
    @Resource
    protected SqlSessionTemplate sqlSessionTemplate;	
	
	@Override
	public List<T> selectByPage(Map<String, Object> params) {		
		return sqlSessionTemplate.selectList(fullQueryName("selectByPage"), params);
	}

	protected String fullQueryName(String queryName) {
		return this.getClass().getName() + "." + queryName;
	}
}
