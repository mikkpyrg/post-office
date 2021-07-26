import api from './PostOfficeAPI';

export const fetchShipments = (lastId) => {
	let url = `/api/shipments`;
	if (lastId) {
		url += `?lastId=${lastId}`;
	}
    return {
        type: 'FETCH_SHIPMENT',
        payload: api.get(url)
    };
};

export const postShipment = (data) => {
    return {
        type: 'POST_SHIPMENT',
        payload: api.post(`/api/shipment`, data)
    };
};

export const finishShipment = (id) => {
    return {
        type: 'POST_SHIPMENT',
        payload: api.post(`/api/shipment/${id}/finish`)
    };
};
