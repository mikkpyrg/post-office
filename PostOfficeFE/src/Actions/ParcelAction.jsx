import api from './PostOfficeAPI';

export const fetchParcels = (bagId, lastId) => {
	let url = `/api/bag/${bagId}/parcels`;
	if (lastId) {
		url += `?lastId=${lastId}`;
	}
    return {
        type: 'FETCH_PARCEL',
        payload: api.get(url)
    };
};

export const postParcel = (data) => {
    return {
        type: 'POST_PARCEL',
        payload: api.post(`/api/parcel`, data)
    };
};
