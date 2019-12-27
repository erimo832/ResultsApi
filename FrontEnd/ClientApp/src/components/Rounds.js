import 'rc-collapse/assets/index.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';

export class Rounds extends Component {
  static displayName = Rounds.name;

  constructor(props) {
    super(props);
    this.state = { rounds: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderRoundsTable(rounds) {
    return (<div>           
      <Collapse
        accordion={false}
      >
        {this.getItems(rounds)}
      </Collapse>
    </div>
    );
  }

  static getItems(rounds) {
    const items = [];
    for (let i = 0, len = rounds.length; i < len; i++) {
      const key = i + 1;
      items.push(
        <Panel header={`${rounds[i].eventName}`} key={key}>
          <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Name</th>
              <th>Place</th>
              <th>Points</th>
              <th>Hcp</th>
              <th>Score</th>              
              <th>HcpScore</th>
            </tr>
          </thead>
          <tbody>
            {rounds[i].results.map(player =>
              <tr key={player.fullName}>
                  <td>{player.fullName}</td>
                  <td>{player.place}</td>
                  <td>{player.points}</td>                  
                  <td>{player.hcp}</td>
                  <td>{player.score}</td>                  
                  <td>{player.hcpScore}</td>
              </tr>
            )}
          </tbody>
        </table>
        </Panel>
      );
    }
    return items;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Rounds.renderRoundsTable(this.state.rounds);
     
    return (
      <div>
        <h1 id="tabelLabel">Rounds</h1>
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
