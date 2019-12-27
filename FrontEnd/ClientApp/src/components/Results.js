import React, { Component } from 'react';

export class Results extends Component {
  static displayName = Results.name;

  constructor(props) {
    super(props);
    this.state = { rounds: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderRoundsTable(rounds) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>eventName</th>
            <th>roundTime</th>
          </tr>
        </thead>
        <tbody>
          {rounds.map(round =>
            <tr key={round.eventName}>
                <td>{round.eventName}</td>
                <td>{round.roundTime}</td>
              
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Results.renderRoundsTable(this.state.rounds);
     

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
    this.setState({ rounds: data, loading: false });
  }
}
