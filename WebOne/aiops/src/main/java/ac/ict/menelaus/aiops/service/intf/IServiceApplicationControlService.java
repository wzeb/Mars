package ac.ict.menelaus.aiops.service.intf;

import java.util.List;

import ac.ict.menelaus.aiops.object.dao.ServiceApplicationControl;

public interface IServiceApplicationControlService {
	public List<ServiceApplicationControl> showPage(String StartDate, String EndDate);

}
