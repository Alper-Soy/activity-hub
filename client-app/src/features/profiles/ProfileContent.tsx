import { observer } from 'mobx-react-lite';
import { Tab, TabPane } from 'semantic-ui-react';
import { Profile } from '../../app/models/profile';
import ProfilePhotos from './ProfilePhotos';

interface Props {
  profile: Profile;
}

export default observer(function ProfileContent({ profile }: Props) {
  const panes = [
    { menuItem: 'About', render: () => <TabPane>About Content</TabPane> },
    { menuItem: 'Photos', render: () => <ProfilePhotos profile={profile} /> },
    { menuItem: 'Events', render: () => <TabPane>Events Content</TabPane> },
    {
      menuItem: 'Followers',
      render: () => <TabPane>Followers Content</TabPane>,
    },
    {
      menuItem: 'Following',
      render: () => <TabPane>Following Content</TabPane>,
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
    />
  );
});
