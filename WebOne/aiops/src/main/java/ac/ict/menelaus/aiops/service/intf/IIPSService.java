package ac.ict.menelaus.aiops.service.intf;

import java.util.List;

import ac.ict.menelaus.aiops.object.dao.IPS;

public interface IIPSService {
	public List<IPS> showPage(Integer offset, Integer count);
}
