package ac.ict.menelaus.aiops.service.intf;

import java.util.List;

import ac.ict.menelaus.aiops.object.dao.ZombieNetwork;

public interface IZombieNetworkService {

	public List<ZombieNetwork> showPage(String StartDate, String EndDate);
}
