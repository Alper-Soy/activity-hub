import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { User, UserFormValues } from '../models/user';
import { router } from '../router/Routes';
import { store } from './store';

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  login = async (creds: UserFormValues) => {
    const user = await agent.Account.login(creds);
    store.commonStore.setToken(user.token);
    runInAction(() => (this.user = user));
    await router.navigate('/activities');
    store.modalStore.closeModal();
  };

  register = async (creds: UserFormValues) => {
    const user = await agent.Account.register(creds);
    store.commonStore.setToken(user.token);
    runInAction(() => (this.user = user));
    await router.navigate('/activities');
    store.modalStore.closeModal();
  };

  logout = async () => {
    store.commonStore.setToken(null);
    this.user = null;
    await router.navigate('/');
  };

  getUser = async () => {
    try {
      const user = await agent.Account.current();
      runInAction(() => (this.user = user));
    } catch (error) {
      console.log(error);
    }
  };

  setImage = (image: string) => {
    if (this.user) this.user.image = image;
  };

  setUserPhoto = (url: string) => {
    if (this.user) this.user.image = url;
  };

  setDisplayName = (name: string) => {
    if (this.user) this.user.displayName = name;
  };
}
