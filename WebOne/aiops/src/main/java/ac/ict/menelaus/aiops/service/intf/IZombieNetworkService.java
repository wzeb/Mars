package ac.ict.menelaus.aiops.service.intf;

import ac.ict.menelaus.aiops.object.dao.ZombieNetwork;

public interface IZombieNetworkService {

	public ZombieNetwork showPage(Integer offset, Integer count);
}
