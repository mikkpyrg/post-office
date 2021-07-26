export default function reducer(state={
    data: {},
    fetching: false,
    fetched:false,
    error: null
}, action) {

    switch (action.type) {
        case "POST_PARCEL_PENDING": {
            return {...state, fetching: true, error: null};
        }
        case "POST_PARCEL_REJECTED": {
            return {...state, fetching: false, error: action.payload.response.data.error};
        }
        case "POST_PARCEL_FULFILLED": {
            return {...state, fetching: false, fetched: true, data: action.payload.data.data};
        }
        default: {}
    }

    return state;
}