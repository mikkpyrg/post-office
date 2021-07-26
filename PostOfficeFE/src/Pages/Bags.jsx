import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { fetchBags } from "../Actions/BagAction";
import BagListItem from "../Components/BagListItem";
class Bags extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            shipmentId: props.match.params.shipmentId,
            lastId: null,
            bags: []
        };
        this.more = this.more.bind(this)
    }
    componentDidMount() {
        this.props.fetchBags(this.state.shipmentId);
    }
    more() {
        this.props.fetchBags(this.state.shipmentId, this.state.lastId);
    }

    componentDidUpdate(oldProps) {
        if (!this.props.bagFetching && oldProps.bagFetching && this.props.bagFetched) {
            this.setState({
                bags: [...this.state.bags, ...this.props.bag],
                lastId: this.props.bag.length < 10 ? null : this.props.bag[9].id
            });
        }
    }

    render() {
        return (
            <div className="container">
                <h1>Bags</h1>
                {this.props.bagPostError &&
                    <div className="error">{this.props.bagPostError}</div>
                }
                <BagListItem data={{
                    bagNumber: "",
                    countOfLetters: 0,
                    weight: 0,
                    price: 0,
                    shipmentId: this.state.shipmentId,
                    bagType: 1,
                    parcelCount: 0,
                    id: null
                }} />
                {this.props.bagFetched && this.state.bags.map((data) =>
                    <BagListItem key={data.id} data={data} />
                )}
                {this.state.lastId && 
                    <button onClick={this.more}>Load more</button>
                }
            </div>
        );
    }
}
function mapStateToProps(state) {
    return {
        bag: state.bag.data,
        bagFetching: state.bag.fetching,
        bagFetched: state.bag.fetched,
        bagError: state.bag.error,


        bagPostError: state.bagPost.error,
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        fetchBags
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(Bags);