package ac.ict.menelaus.aiops.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import ac.ict.menelaus.aiops.dal.intf.IZombieNetworkDal;
import ac.ict.menelaus.aiops.object.dao.ZombieNetwork;
import ac.ict.menelaus.aiops.service.intf.IZombieNetworkService;

@Service
public class ZombieNetworkService implements IZombieNetworkService {

	@Resource
	private IZombieNetworkDal zombieNetworkDal;
	
	@Override
	public List<ZombieNetwork>  showPage(String StartDate, String EndDate) {
		Map<String, Object> params = new HashMap<>();
		params.put("StartDate", StartDate);
		params.put("EndDate", EndDate);
		return zombieNetworkDal.selectByPage(params);
	}

}
