import { LeaderboardUiPage } from './app.po';

describe('leaderboard-ui App', function() {
  let page: LeaderboardUiPage;

  beforeEach(() => {
    page = new LeaderboardUiPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
