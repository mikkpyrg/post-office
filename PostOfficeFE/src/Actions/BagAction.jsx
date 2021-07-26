import api from './PostOfficeAPI';

export const fetchBags = (shipmentId, lastId) => {
	let url = `/api/shipment/${shipmentId}/bags`;
	if (lastId) {
		url += `?lastId=${lastId}`;
	}
    return {
        type: 'FETCH_BAG',
        payload: api.get(url)
    };
};

export const postBag = (data) => {
    return {
        type: 'POST_BAG',
        payload: api.post(`/api/bag`, data)
    };
};
