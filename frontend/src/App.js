import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { };
    }

   
    }

    render() {
        <></>
    }

    async populateWeatherData() {
        const response = await fetch('api/producto');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}
