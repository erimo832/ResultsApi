import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";

export class ScoreLeaderboard extends Component {
  static displayName = ScoreLeaderboard.name;

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
                   <div>{i18n.t('series_basednumrounds', {cnt: series[i].roundsToCount } )}</div>
               </Col>
            </Row>
            <Row>            
              <Col sm={12} lg={12}>                
                <table className='table table-striped' aria-labelledby="tabelLabel">
                  <thead>
                    <tr>              
                      <th>{i18n.t('column_place')}</th>
                      <th>{i18n.t('column_name')}</th>
                      <th>{i18n.t('column_avgscore')}</th>                      
                      <th className="d-none d-lg-table-cell">{i18n.t('column_totalscore')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_minthrows')}</th>
                      <th className="d-none d-sm-table-cell">{i18n.t('column_maxthrows')}</th>
                      <th className="d-none d-lg-table-cell">{i18n.t('column_rounds')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {series[i].placements.map(player =>
                      <tr key={player.fullName}>
                          <td>{player.place}</td>
                          <td>{player.fullName}</td>                  
                          <td>{player.avgScore}</td>                  
                          <td className="d-none d-lg-table-cell">{player.totalScore}</td>                                                    
                          <td className="d-none d-sm-table-cell">{player.topResults[0].score}</td>
                          <td className="d-none d-sm-table-cell">{player.topResults[player.topResults.length - 1].score}</td>
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
      : ScoreLeaderboard.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_score_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('http://orbitibro.ddns.net:8088/api/Series/scoreLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}
