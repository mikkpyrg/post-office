import { createStore, applyMiddleware } from "redux";
import reducer from "./Reducers";
import thunk from "redux-thunk";
import promise from "redux-promise-middleware";
const middleware = applyMiddleware(promise, thunk);

const store = createStore(reducer, middleware);
export default store;