export default function reducer(state={
    data: {},
    fetching: false,
    fetched:false,
    error: null
}, action) {

    switch (action.type) {
        case "FETCH_PARCEL_PENDING": {
            return {...state, fetching: true, error: null};
        }
        case "FETCH_PARCEL_REJECTED": {
            return {...state, fetching: false, error: action.payload.response.data.error};
        }
        case "FETCH_PARCEL_FULFILLED": {
            return {...state, fetching: false, fetched: true, data: action.payload.data.data};
        }
        default: {}
    }

    return state;
}