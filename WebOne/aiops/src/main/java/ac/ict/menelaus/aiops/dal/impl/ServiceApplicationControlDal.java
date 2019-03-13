package ac.ict.menelaus.aiops.dal.impl;

import org.springframework.stereotype.Repository;

import ac.ict.menelaus.aiops.dal.intf.IServiceApplicationControlDal;
import ac.ict.menelaus.aiops.object.dao.ServiceApplicationControl;

@Repository
public class ServiceApplicationControlDal 
extends BaseDao<ServiceApplicationControl>
implements IServiceApplicationControlDal {
}
