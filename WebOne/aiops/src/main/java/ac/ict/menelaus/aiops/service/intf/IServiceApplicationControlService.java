package ac.ict.menelaus.aiops.service.intf;

import ac.ict.menelaus.aiops.object.dao.ServiceApplicationControl;

public interface IServiceApplicationControlService {
	public ServiceApplicationControl showPage(Integer offset, Integer count);

}
