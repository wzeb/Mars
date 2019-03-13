package ac.ict.menelaus.aiops.service.impl;

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
	public ZombieNetwork showPage(Integer offset, Integer count) {
		// TODO Auto-generated method stub
		return null;
	}

}
