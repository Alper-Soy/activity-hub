import { observer } from 'mobx-react-lite';
import { useParams } from 'react-router-dom';
import ProfileHeader from './ProfileHeader';
import ProfileContent from './ProfileContent';
import { Grid } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';
import { useEffect } from 'react';
import LoadingComponent from '../../app/layout/LoadingComponent';

export default observer(function ProfilePage() {
  const { username } = useParams<{ username: string }>();
  const { profileStore } = useStore();
  const { loadingProfile, loadProfile, profile } = profileStore;

  useEffect(() => {
    if (username) loadProfile(username);
  }, [loadProfile, username]);

  if (loadingProfile)
    return <LoadingComponent inverted content="Loading profile..." />;

  if (!profile) return <h2>Problem loading profile</h2>;

  return (
    <Grid>
      <Grid.Column width="16">
        <ProfileHeader profile={profile} />
        <ProfileContent profile={profile} />
      </Grid.Column>
    </Grid>
  );
});
