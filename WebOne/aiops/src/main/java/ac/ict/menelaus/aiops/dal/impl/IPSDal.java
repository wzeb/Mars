package ac.ict.menelaus.aiops.dal.impl;

import org.springframework.stereotype.Repository;

import ac.ict.menelaus.aiops.dal.intf.IIPSDal;
import ac.ict.menelaus.aiops.object.dao.IPS;

@Repository
public class IPSDal extends BaseDao<IPS> implements IIPSDal {
}
