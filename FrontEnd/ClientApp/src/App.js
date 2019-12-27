import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Results } from './components/Results';
import { Rounds } from './components/Rounds';

import './custom.css'


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />        
        <Route path='/results' component={Results} />
        <Route path='/rounds' component={Rounds} />
      </Layout>
    );
  }
}
