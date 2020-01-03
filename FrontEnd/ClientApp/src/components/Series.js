import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../i18n";

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
          <Container>
            <Row>
              <Col sm={12} lg={12}>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                  <thead>
                    <tr>              
                      <th>Place</th>
                      <th>Name</th>
                      <th>TotalPoints</th>
                      <th className="d-none d-md-table-cell">AvgPoints</th>                                
                      <th className="d-none d-sm-table-cell">AvgHcpScore</th> 
                      <th className="d-none d-md-table-cell">TotalHcpScore</th>
                      <th className="d-none d-lg-table-cell">NumberOfRounds</th>
                    </tr>
                  </thead>
                  <tbody>
                    {series[i].placements.map(player =>
                      <tr key={player.fullName}>
                          <td>{player.place}</td>
                          <td>{player.fullName}</td>                  
                          <td>{player.totalPoints}</td>                  
                          <td className="d-none d-md-table-cell">{player.avgPoints}</td>                          
                          <td className="d-none d-sm-table-cell">{player.avgHcpScore}</td>
                          <td className="d-none d-md-table-cell">{player.totalHcpScore}</td>                  
                          <td className="d-none d-lg-table-cell">{player.numberOfRounds}</td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </Col>              
            </Row>
            
        </Container>
        </Panel>
      );
    }
    return items;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : Series.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('series_header')}</h1>
        <p>{i18n.t('series_description')}</p>
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
