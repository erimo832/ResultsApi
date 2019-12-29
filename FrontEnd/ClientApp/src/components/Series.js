import 'rc-collapse/assets/index.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';

export class Series extends Component {
  static displayName = Series.name;

  constructor(props) {
    super(props);
    this.state = { series: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderSeriesTable(series) {
    return (<div>           
      <Collapse
        accordion={false}
      >
        {this.getItems(series)}
      </Collapse>
    </div>
    );
  }

  static getItems(series) {
    const items = [];
    for (let i = 0, len = series.length; i < len; i++) {
      const key = i + 1;
      items.push(
        <Panel header={`${series[i].serieName}`} key={key}>
          <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>              
              <th>Place</th>
              <th>Name</th>
              <th>TotalPoints</th>
              <th>AvgPoints</th>
              <th>TotalHcpScore</th>              
              <th>AvgHcpScore</th> 
              <th>NumberOfRounds</th>
            </tr>
          </thead>
          <tbody>
            {series[i].placements.map(player =>
              <tr key={player.fullName}>
                  <td>{player.place}</td>
                  <td>{player.fullName}</td>                  
                  <td>{player.totalPoints}</td>                  
                  <td>{player.avgPoints}</td>
                  <td>{player.totalHcpScore}</td>                  
                  <td>{player.avgHcpScore}</td>
                  <td>{player.numberOfRounds}</td>
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
      : Series.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">Series</h1>
        <p>Some info</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://ceptor.myftp.org:8088/api/Series/hcpLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}
