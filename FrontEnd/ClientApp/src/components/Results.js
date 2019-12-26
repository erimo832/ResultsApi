import React, { Component } from 'react';

export class Results extends Component {
  static displayName = Results.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>eventName</th>
            <th>roundTime</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.eventName}>
                <td>{forecast.eventName}</td>
                <td>{forecast.roundTime}</td>
              
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Results.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Rounds</h1>
        <p>Some info</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://ceptor.myftp.org:8088/api/Rounds');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
