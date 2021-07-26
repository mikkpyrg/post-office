import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { postParcel } from "../Actions/ParcelAction";

class ParcelListItem extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            showContent: false
        };
        this.onChange = this.onChange.bind(this);
        this.toggleContent = this.toggleContent.bind(this)
        this.onPost = this.onPost.bind(this)
    }

    toggleContent() {
        this.setState({showContent: !this.state.showContent})
    }

    onChange(e) {
        var data = {...this.state.data}
        data[e.target.name] = e.target.value;
        this.setState({data})
    }

    onPost() {
        this.props.postParcel(this.state.data)
        .then(() => {
            location.reload();
        });
    }

    render() {
        const data = this.state.data;
        return (
            <>
                <div className="list-item-header" onClick={this.toggleContent}>
                    <span>{data.id ? data.parcelNumber : "Create New"}</span>
                </div>
                {this.state.showContent &&
                    <div className="list-item-content">
                        <span>Parcel number</span>
                        <input type="text" name="parcelNumber" value={data.parcelNumber} onChange={this.onChange} />
                        <span>Recipient Name</span>
                        <input type="text" name="recipientName" value={data.recipientName} onChange={this.onChange} />
                        <span>Destination Country</span>
                        <input type="text" name="destinationCountry" value={data.destinationCountry} onChange={this.onChange} />
                        <span>Weight</span>
                        <input type="number" name="weight" min="0" value={data.weight} onChange={this.onChange} />
                        <span>Price</span>
                        <input type="number" name="price" min="0" value={data.price} onChange={this.onChange} />
                        <button onClick={this.onPost}>Update</button>
                    </div>
                }
            </>
        );
    }
}
function mapStateToProps(state) {
    return {
        parcel: state.parcelPost.data
    };
}

function matchDispatchToProps(dispatch) {
    return bindActionCreators({
        postParcel
    }, dispatch);
}

export default connect(mapStateToProps, matchDispatchToProps)(ParcelListItem);