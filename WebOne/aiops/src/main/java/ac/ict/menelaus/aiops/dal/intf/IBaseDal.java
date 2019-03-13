package ac.ict.menelaus.aiops.dal.intf;

import java.util.List;
import java.util.Map;

public interface IBaseDal<T> {
	public List<T> selectByPage(Map<String, Object> params);
}
