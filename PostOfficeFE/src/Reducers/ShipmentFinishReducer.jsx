export default function reducer(state={
    data: {},
    fetching: false,
    fetched:false,
    error: null
}, action) {

    switch (action.type) {
        case "FINISH_SHIPMENT_PENDING": {
            return {...state, fetching: true, error: null};
        }
        case "FINISH_SHIPMENT_REJECTED": {
            return {...state, fetching: false, error: action.payload.response.data.error};
        }
        case "FINISH_SHIPMENT_FULFILLED": {
            return {...state, fetching: false, fetched: true, data: action.payload.data.data};
        }
        default: {}
    }

    return state;
}