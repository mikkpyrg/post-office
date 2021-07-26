import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { fetchParcels } from "../Actions/ParcelAction";
import ParcelListItem from "../Components/ParcelListItem";
class Parcels extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            bagId: props.match.params.bagId,
            lastId: null,
            parcels: []
        };
        this.more = this.more.bind(this)
    }
    componentDidMount() {
        this.props.fetchParcels(this.state.bagId);
    }
    more() {
        this.props.fetchParcels(this.state.bagId, this.state.lastId);
    }

    componentDidUpdate(oldProps) {
        if (!this.props.parcelFetching && oldProps.parcelFetching && this.props.parcelFetched) {
            this.setState({
                parcels: [...this.state.parcels, ...this.props.parcel],
                lastId: this.props.parcel.length < 10 ? null : this.props.parcel[9].id
            });
        }
    }

    render() {
        return (
            <div className="container">
                <h1>Parcels</h1>
                {this.props.parcelPostError &&
                    <div className="error">{this.props.parcelPostError}</div>
                }
                <ParcelListItem data={{
                    parcelNumber: "",
                    recipientName: "",
                    destinationCountry: "",
                    weight: 0,
                    price: 0,
                    bagId: this.state.bagId,
                    id: null
                }} />
                {this.props.parcelFetched && this.state.parcels.map((data) =>
                    <ParcelListItem key={data.id} data={data} />
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
        parcel: state.parcel.data,
        parcelFetching: state.parcel.fetching,
        parcelFetched: state.parcel.fetched,
        parcelError: state.parcel.error,


        parcelPostError: state.parcelPost.error,
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        fetchParcels
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(Parcels);