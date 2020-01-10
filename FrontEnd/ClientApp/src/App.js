import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Hcp } from './components/Hcp';
import { Rounds } from './components/Rounds';
import { Series } from './components/Series';
import './custom.css';

export default class App extends Component {
  static displayName = App.name;
  
  render () {    
    const { t } = this.props;
    return (
      <Layout t={t}>
        <Route exact path='/' component={Home} />
        <Route path='/hcp/' component={Hcp} />
        <Route path='/rounds' component={Rounds} />
        <Route path='/series' component={Series} />
      </Layout>
    );
  }
}