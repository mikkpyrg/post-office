import { combineReducers } from "redux";

import shipment from "./ShipmentReducer";
import shipmentPost from "./ShipmentPostReducer"
import shipmentFinish from "./ShipmentFinishReducer"
import bag from "./BagReducer";
import bagPost from "./BagPostReducer"
import parcel from "./ParcelReducer";
import parcelPost from "./ParcelPostReducer"
const allReducers = combineReducers({
    shipment,
    shipmentPost,
    shipmentFinish,
    bag,
    bagPost,
    parcel,
    parcelPost
});
export default allReducers;
